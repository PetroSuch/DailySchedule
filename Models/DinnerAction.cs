using System;

namespace Task.Models
{
    public class DinnerAction : ActionItem
    {
        public DinnerAction(int duration, TimeOnly startTime)
            : base(
                "Dinner",
                duration,
                startTime,
                ActionType.Dinner
            )
        {
        }
    }
}