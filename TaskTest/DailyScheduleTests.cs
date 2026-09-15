using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task.Models;

namespace Task.Tests
{
    [TestClass]
    public class DailyScheduleTests
    {
        [TestMethod]
        public void AddAction_ShouldAddActionToList()
        {
            DailySchedule schedule = new DailySchedule();

            WalkAction action = new WalkAction(30, new TimeOnly(10, 0));

            schedule.AddAction(0, action);

            Assert.AreEqual(1, schedule.Actions.Count);
        }

        [TestMethod]
        public void AddAction_ShouldSetCorrectActionType()
        {
            DailySchedule schedule = new DailySchedule();

            WalkAction action = new WalkAction(30, new TimeOnly(10, 0));

            schedule.AddAction(0, action);

            Assert.AreEqual(ActionType.Walk, schedule.Actions[0].Type);
        }
    }
}