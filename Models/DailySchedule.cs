using System;
using System.Collections.Generic;

namespace Task.Models
{
    public class DailySchedule
    {
        private DateTime _date;

        private List<ActionItem> _actions =
            new List<ActionItem>();

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public List<ActionItem> Actions
        {
            get { return _actions; }
            set { _actions = value; }
        }

        public void AddAction(
            int addAfterIndex,
            ActionItem action)
        {
            _actions.Insert(
                addAfterIndex,
                action
            );
        }
    }
}