using System;
using System.Collections.Generic;
using System.Text;

namespace Task
{
    internal class DailySchedule
    {
        private DateTime _date;
        private List<Action> _actions = new List<Action>();

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public List<Action> Actions
        {
            get { return _actions; }
            set {  _actions  = value; }
        }

        public void addAction(int addAfterIndex, Action action)
        {
            _actions.Insert(addAfterIndex, action);
        }
    }
}
