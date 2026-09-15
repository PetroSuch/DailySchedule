using System;
using System.Collections.Generic;

namespace Task.Data
{
    public class DailyScheduleData
    {
        public DateTime Date { get; set; }

        public List<ActionData> Actions { get; set; }
    }
}