using System;

namespace Task.Models
{
    public class SleepAction : ActionItem
    {
        public SleepAction(int duration, TimeOnly startTime)
            : base(
                "Sleep",
                duration,
                startTime,
                ActionType.Sleep
            )
        {
        }
    }
}