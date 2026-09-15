using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task.Logic;
using Task.Models;

namespace SchedulerTests
{
    [TestClass]
    public class DailySchedulerHelperTests
    {
        [TestMethod]
        public void CalculateWalkingDuration_ShouldReturnTotalWalkingTime()
        {
            DailySchedule schedule = new DailySchedule();

            schedule.Actions.Add(new WalkAction(30, new TimeOnly(9, 0)));
            schedule.Actions.Add(new WalkAction(60, new TimeOnly(17, 0)));

            DailySchedulerHelper helper = new DailySchedulerHelper();

            int result = helper.CalculateWalkingDuration(schedule);

            Assert.AreEqual(90, result);
        }
    }
}