using System;

namespace Task.Models
{
    public class LunchAction : ActionItem
    {
        public LunchAction(int duration, TimeOnly startTime)
            : base(
                "Lunch",
                duration,
                startTime,
                ActionType.Lunch
            )
        {
        }
    }
}