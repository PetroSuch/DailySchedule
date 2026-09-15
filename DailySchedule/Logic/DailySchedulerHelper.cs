using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Task.Data;
using Task.Models;
using Task.UI;

namespace Task.Logic
{
    public class DailySchedulerHelper
    {
        private const int RequiredWalkingMinutes = 120;
        private List<DailySchedule> _data = new List<DailySchedule>();
        private Dictionary<string, string> _dayNames = new Dictionary<string, string>();

        private readonly FileInfo _daysFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "Data", "days.json"));

        public List<DailySchedule> Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public DailySchedulerHelper()
        {
            DailyScheduleDataReader reader = new DailyScheduleDataReader();

            Data = reader.GetData();

            LoadDayNames();
        }

        private void LoadDayNames()
        {
            try
            {
                if (!_daysFile.Exists)
                {
                    Console.WriteLine($"File not found: {_daysFile.FullName}");
                    return;
                }

                using (StreamReader reader = new StreamReader(_daysFile.FullName))
                {
                    string json = reader.ReadToEnd();

                    Dictionary<string, string> days = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                    if (days != null)
                    {
                        _dayNames = days;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error while reading days file: {ex.Message}");
            }
        }

        public DailySchedule GetDataPerDay(DayOfWeek day)
        {
            return Data.FirstOrDefault(item => item.Date.DayOfWeek == day && item.Actions.Count > 0);
        }

        public void CalculateWalkingTimePerDay()
        {
            Console.WriteLine("Введіть назву дня");

            DayOfWeek day;

            try
            {
                string input = Console.ReadLine() ?? "";

                day = ParseUserInputDay(input);
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                return;
            }

            DailySchedule dataPerDay = GetDataPerDay(day);

            if (dataPerDay == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Для цього дня немає даних.");
                Console.ResetColor();

                return;
            }

            int walkingDuration = CalculateWalkingDuration(dataPerDay);

            if (walkingDuration < RequiredWalkingMinutes)
            {
                int additionalWalkingTime = RequiredWalkingMinutes - walkingDuration;

                ReplaceWatchingTvWithWalking(dataPerDay, additionalWalkingTime);
            }

            ScheduleWriter writer = new ScheduleWriter();

            writer.Write(dataPerDay);

            CalculateWalkingDuration(dataPerDay);
        }

        private void ReplaceWatchingTvWithWalking(DailySchedule dataPerDay, int additionalWalkingTime)
        {
            for (int i = 0; i < dataPerDay.Actions.Count; i++)
            {
                ActionItem action = dataPerDay.Actions[i];

                if (action.StartTime.Hour >= 13 && action.Type == ActionType.WatchingTV)
                {
                    int walkingTime = Math.Min(additionalWalkingTime, action.Duration);

                    int remainingWatchingTvTime = action.Duration - walkingTime;

                    TimeOnly originalStartTime = action.StartTime;

                    dataPerDay.Actions[i] = new WalkAction(walkingTime, originalStartTime);

                    if (remainingWatchingTvTime > 0)
                    {
                        TimeOnly watchingTvStartTime = originalStartTime.AddMinutes(walkingTime);

                        WatchingTvAction watchingTv = new WatchingTvAction(
                            remainingWatchingTvTime,
                            watchingTvStartTime
                        );

                        dataPerDay.AddAction(i + 1, watchingTv);
                    }

                    break;
                }
            }
        }

        public int CalculateWalkingDuration(DailySchedule dataPerDay)
        {
            int walkingDuration = dataPerDay.Actions
                .Where(action => action.Type == ActionType.Walk)
                .Sum(action => action.Duration);

            DayOfWeek day = dataPerDay.Date.DayOfWeek;
            string dayName = GetDayOfWeek(day);

            string message = $"Загальна тривалість прогулянок у {dayName} ({dataPerDay.Date:yyyy-MM-dd}) становить: {walkingDuration} хв.";

            Console.WriteLine(message);

            try
            {
                using StreamWriter writer = new StreamWriter("WalkingDuration.txt");

                writer.WriteLine(message);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error while writing walking duration: {ex.Message}");
            }

            return walkingDuration;
        }

        private string GetDayOfWeek(DayOfWeek day)
        {
            string key = day.ToString();

            if (_dayNames.ContainsKey(key))
            {
                return _dayNames[key];
            }

            return key;
        }

        private DayOfWeek ParseUserInputDay(string input)
        {
            input = input.Trim().ToLower();

            switch (input)
            {
                case "mon":
                case "monday":
                case "понеділок":
                case "пн":
                    return DayOfWeek.Monday;

                case "tue":
                case "tuesday":
                case "вівторок":
                case "вт":
                    return DayOfWeek.Tuesday;

                case "wed":
                case "wednesday":
                case "середа":
                case "ср":
                    return DayOfWeek.Wednesday;

                case "thu":
                case "thursday":
                case "четвер":
                case "чт":
                    return DayOfWeek.Thursday;

                case "fri":
                case "friday":
                case "п'ятниця":
                case "пт":
                    return DayOfWeek.Friday;

                case "sat":
                case "saturday":
                case "субота":
                case "сб":
                    return DayOfWeek.Saturday;

                case "sun":
                case "sunday":
                case "неділя":
                case "нд":
                    return DayOfWeek.Sunday;

                default:
                    throw new ArgumentException($"Не можу знайти такого дня: {input}");
            }
        }
    }
}