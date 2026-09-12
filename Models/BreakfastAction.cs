using System;

namespace Task.Models
{
    public class BreakfastAction : ActionItem
    {
        public BreakfastAction(int duration, TimeOnly startTime)
            : base(
                "Breakfast",
                duration,
                startTime,
                ActionType.Breakfast
            )
        {
        }
    }
}