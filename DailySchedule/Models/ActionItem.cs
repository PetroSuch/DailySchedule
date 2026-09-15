using System;
using Task.Interfaces;

namespace Task.Models
{
    public class ActionItem : IActionItem
    {
        private string _name = "";
        private int _duration;
        private TimeOnly _startTime;

        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public int Duration
        {
            get { return _duration; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Duration must be greater than 0.");
                }

                _duration = value;
            }
        }

        public TimeOnly StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public ActionType Type { get; set; }

        public ActionItem(ActionType type, int duration, TimeOnly startTime)
        {
            Name = type.ToString();
            Duration = duration;
            StartTime = startTime;
            Type = type;
        }
    }
}