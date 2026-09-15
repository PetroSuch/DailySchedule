using System;

namespace Task.Models
{
    public class WatchingTvAction : ActionItem
    {
        public WatchingTvAction(int duration, TimeOnly startTime)
            : base(ActionType.WatchingTV, duration, startTime)
        {
        }
    }
}