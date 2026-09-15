using System;

namespace Task.Models
{
    public class DinnerAction : ActionItem
    {
        public DinnerAction(int duration, TimeOnly startTime)
            : base(ActionType.Dinner, duration, startTime)
        {
        }
    }
}