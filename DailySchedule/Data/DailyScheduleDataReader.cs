using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Task.Models;

namespace Task.Data
{
    public class DailyScheduleDataReader
    {
        private readonly FileInfo _scheduleFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "Data", "daily-schedule.json"));

        public List<DailySchedule> GetData()
        {
            List<DailySchedule> result = new List<DailySchedule>();

            try
            {
                if (!_scheduleFile.Exists)
                {
                    Console.WriteLine("File not found.");
                    return result;
                }

                using (StreamReader reader = new StreamReader(_scheduleFile.FullName))
                {
                    string json = reader.ReadToEnd();

                    JsonSerializerOptions options = new JsonSerializerOptions();

                    options.PropertyNameCaseInsensitive = true;

                    List<DailyScheduleData> schedules = JsonSerializer.Deserialize<List<DailyScheduleData>>(json, options);

                    if (schedules == null)
                    {
                        return result;
                    }

                    foreach (DailyScheduleData schedule in schedules)
                    {
                        DailySchedule dailySchedule = new DailySchedule();

                        dailySchedule.Date = schedule.Date;

                        foreach (ActionData action in schedule.Actions)
                        {
                            ActionItem newAction = CreateAction(action);

                            dailySchedule.Actions.Add(newAction);
                        }

                        result.Add(dailySchedule);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"File error: {ex.Message}");
            }

            return result;
        }

        private ActionItem CreateAction(ActionData action)
        {
            TimeOnly startTime = TimeOnly.Parse(action.StartTime);

            switch (action.Name)
            {
                case "Walk":
                    return new WalkAction(action.Duration, startTime);

                case "Watching TV":
                    return new WatchingTvAction(action.Duration, startTime);

                case "Breakfast":
                    return new BreakfastAction(action.Duration, startTime);

                case "Study":
                    return new StudyAction(action.Duration, startTime);

                case "Lunch":
                    return new LunchAction(action.Duration, startTime);

                case "Dinner":
                    return new DinnerAction(action.Duration, startTime);

                case "Sleep":
                    return new SleepAction(action.Duration, startTime);

                default:
                    return new ActionItem(ActionType.Unknown, action.Duration, startTime);
            }
        }
    }
}