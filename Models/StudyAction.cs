using System;

namespace Task.Models
{
    public class StudyAction : ActionItem
    {
        public StudyAction(int duration, TimeOnly startTime)
            : base(
                "Study",
                duration,
                startTime,
                ActionType.Study
            )
        {
        }
    }
}