using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Task
{
    internal class DailySchedulerHelper
    {
        private List<DailySchedule> _data = new List<DailySchedule>();
        private readonly string _filePath = @"C:\Users\User\Desktop\Курси\Task\daily-schedule.json";

        public List<DailySchedule> Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public DailySchedulerHelper()
        {
            getData();
        }

        private List<DailySchedule> getData()
        {
            List<DailySchedule> data = new List<DailySchedule>();

            try
            {
                using (StreamReader reader = new StreamReader(_filePath))
                {
                    string json = reader.ReadToEnd();

                    Data = JsonSerializer.Deserialize<List<DailySchedule>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                    );
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return data;
        }

        public DailySchedule getDataPerDay(DayOfWeek day)
        {
            DailySchedule result = null;

            foreach(DailySchedule item in Data)
            {
                DateTime date = item.Date;
                DayOfWeek dayNumber = date.DayOfWeek;

                if(dayNumber == day && item.Actions.Count() > 0)
                {
                    result = item;
                }
            }

            return result;
        }

        public void calculateWalkingTimePerDay()
        {
            Console.WriteLine("Введіть назву дня");
            DayOfWeek day;

            try
            {
                day = ParseUserInputDay(Console.ReadLine());
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                calculateWalkingTimePerDay();
                return;
            }

            DailySchedule dataPerDay = getDataPerDay(day);

            int walkingDuration = calculateWalkingDuration(dataPerDay);

            if(walkingDuration < 120)
            {
                int additionalWalkingTime = 120 - walkingDuration;
                bool isFoundWatchingTVAfterLunch = false;

                for (int i = 0; i < dataPerDay.Actions.Count; i++)
                {
                    Action action = dataPerDay.Actions[i];

                    if(action.StartTime.Hour >= 13 && !isFoundWatchingTVAfterLunch)
                    {
                        int remainingTimeForWatchingTV = action.Duration - additionalWalkingTime;

                        // Replace the activity Watching TV with the Walking one
                        if (action.Name == "Watching TV" && !isFoundWatchingTVAfterLunch)
                        {
                            isFoundWatchingTVAfterLunch = true;
                            action.Name = "Walk";
                            action.Duration = additionalWalkingTime;
                        }

                        // Check if there is some time remaining after walking to watch TV
                        if (remainingTimeForWatchingTV > 0)
                        {
                            TimeOnly startTime = action.StartTime.AddMinutes(additionalWalkingTime);
                            Action newAction = new Action("Watching TV", remainingTimeForWatchingTV, startTime);
                            dataPerDay.addAction(i + 1, newAction);
                        }
                    }
                }
            }

            try
            {
                using (StreamWriter writer = new StreamWriter("DaySchedule.txt"))
                {
                    foreach (Action action in dataPerDay.Actions)
                    {
                        string formattedDuration = GetFormattedDuration(action.Duration);

                        string msg = $"Дія: {action.Name,-12} Початок: {action.StartTime,-7} Тривалість: {formattedDuration}";
                        writer.WriteLine(msg);

                        if (action.Name == "Walk") Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(msg);
                        Console.ResetColor();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while preparing list of activities: ${ex.Message}");
            }

            calculateWalkingDuration(dataPerDay);
        }

        private int calculateWalkingDuration(DailySchedule dataPerDay)
        {
            int walkingDuration = 0;
            DayOfWeek day = dataPerDay.Date.DayOfWeek;
            foreach (Action action in dataPerDay.Actions)
            {
                if (action.Name == "Walk")
                {
                    walkingDuration += action.Duration;
                }
            }

            try
            {
                using (StreamWriter writer = new StreamWriter("WalkingDuration.txt"))
                {
                    string message = $"Загальна тривалість прогулянок у {getDayOfWeek(day)} ({dataPerDay.Date.ToString("yyyy-MM-dd")}) становить: {walkingDuration} хв.";
                    Console.WriteLine(message);
                    writer.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while calculation total amount of walking duration: ${ex.Message}");
            }

            return walkingDuration;
        }

        private string GetFormattedDuration(int minutes)
        {
            if (minutes < 60)
            {
                return $"{minutes}хв";
            }

            int hours = minutes / 60;
            int remainingMinutes = minutes % 60;

            if (remainingMinutes == 0)
            {
                return $"{hours}г";
            }

            return $"{hours}г {remainingMinutes}хв";
        }

        private string getDayOfWeek(DayOfWeek day)
        {
            switch(day)
            {
                case DayOfWeek.Monday:
                    return "понеділок";
                case DayOfWeek.Tuesday:
                    return "вівторок";
                case DayOfWeek.Wednesday:
                    return "середа";
                case DayOfWeek.Thursday:
                    return "четвер";
                case DayOfWeek.Friday:
                    return "п'ятниця";
                case DayOfWeek.Saturday:
                    return "суботф";
                case DayOfWeek.Sunday:
                    return "неділя";
                default:
                    return "";
            }
        }

        private DayOfWeek ParseUserInputDay(string input)
        {
            DayOfWeek day;
            switch (input)
            {
                case "mon":
                case "monday":
                case "понеділок":
                case "пн":
                    day = DayOfWeek.Monday;
                    break;

                case "tue":
                case "tuesday":
                case "вівторок":
                case "вт":
                    day = DayOfWeek.Tuesday;
                    break;

                case "wed":
                case "wednesday":
                case "середа":
                case "ср":
                    day = DayOfWeek.Wednesday;
                    break;

                case "thu":
                case "thursday":
                case "четвер":
                case "чт":
                    day = DayOfWeek.Thursday;
                    break;

                case "fri":
                case "friday":
                case "п'ятниця":
                case "пт":
                    day = DayOfWeek.Friday;
                    break;

                case "sat":
                case "saturday":
                case "субота":
                case "сб":
                    day = DayOfWeek.Saturday;
                    break;

                case "sun":
                case "sunday":
                case "неділя":
                case "нд":
                    day = DayOfWeek.Sunday;
                    break;

                default:
                    throw new Exception($"Не можу знайти такого дня: {input}");
            }

            return day;
        }
    }
}
