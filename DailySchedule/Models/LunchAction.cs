using System;

namespace Task.Models
{
    public class LunchAction : ActionItem
    {
        public LunchAction(int duration, TimeOnly startTime)
            : base(ActionType.Lunch, duration, startTime)
        {
        }
    }
}