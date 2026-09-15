using System;

namespace Task.Models
{
    public class StudyAction : ActionItem
    {
        public StudyAction(int duration, TimeOnly startTime)
            : base(ActionType.Study, duration, startTime)
        {
        }
    }
}