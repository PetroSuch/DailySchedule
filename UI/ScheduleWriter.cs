using System;
using System.IO;
using Task.Models;

namespace Task.UI
{
    internal class ScheduleWriter
    {
        private const int MinutesPerHour = 60;

        public void Write(
            DailySchedule schedule)
        {
            try
            {
                using StreamWriter writer =
                    new StreamWriter(
                        "DaySchedule.txt"
                    );

                foreach (
                    ActionItem action
                    in schedule.Actions
                )
                {
                    string duration =
                        GetFormattedDuration(
                            action.Duration
                        );

                    string message =
                        $"Дія: {action.Name,-12} " +
                        $"Початок: {action.StartTime,-7} " +
                        $"Тривалість: {duration}";

                    writer.WriteLine(message);

                    if (
                        action.Type ==
                        ActionType.Walk
                    )
                    {
                        Console.ForegroundColor =
                            ConsoleColor.Green;
                    }

                    Console.WriteLine(message);
                    Console.ResetColor();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(
                    ex.Message
                );
            }
        }

        private string GetFormattedDuration(
            int minutes)
        {
            int hours =
                minutes / MinutesPerHour;

            int remainingMinutes =
                minutes % MinutesPerHour;

            if (minutes < MinutesPerHour)
            {
                return $"{minutes}хв";
            }

            if (remainingMinutes == 0)
            {
                return $"{hours}г";
            }

            return
                $"{hours}г {remainingMinutes}хв";
        }
    }
}