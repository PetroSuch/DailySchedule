using System;
using System.Collections.Generic;
using System.Text;

namespace Task
{
    internal class Action
    {
        private string _name = "";
        private int _duration = 0;
        private TimeOnly _startTime;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public TimeOnly StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public Action(string name, int duration, TimeOnly startTime)
        {
            _name = name;
            _duration = duration;
            _startTime = startTime;
        }
    }
}
