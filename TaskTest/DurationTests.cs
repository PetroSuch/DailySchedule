using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task.Models;

namespace SchedulerTests
{
    [TestClass]
    public class DurationTests
    {
        [TestMethod]
        public void Duration_ShouldThrowException_WhenValueIsZero()
        {
            ActionItem action = new ActionItem(ActionType.Unknown, 10, new TimeOnly(10, 0));

            bool exceptionWasThrown = false;

            try
            {
                action.Duration = 0;
            }
            catch (ArgumentException)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
        }

        [TestMethod]
        public void Duration_ShouldSetValue_WhenValueIsPositive()
        {
            ActionItem action = new ActionItem(
                ActionType.Unknown,
                10,
                new TimeOnly(10, 0)
            );

            action.Duration = 20;

            Assert.AreEqual(20, action.Duration);
        }
    }
}