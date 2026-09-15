using System;

namespace Task.Models
{
    public class WalkAction : ActionItem
    {
        public WalkAction(int duration, TimeOnly startTime)
            : base(ActionType.Walk, duration, startTime)
        {
        }
    }
}