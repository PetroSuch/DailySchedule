using System;

namespace Task.Models
{
    public class WatchingTvAction : ActionItem
    {
        public WatchingTvAction(int duration, TimeOnly startTime)
            : base(
                "Watching TV",
                duration,
                startTime,
                ActionType.WatchingTV
            )
        {
        }
    }
}