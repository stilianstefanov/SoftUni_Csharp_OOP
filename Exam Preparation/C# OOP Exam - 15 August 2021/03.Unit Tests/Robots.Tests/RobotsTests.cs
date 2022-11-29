namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class RobotsTests
    {
        private Robot robot;
        private RobotManager robotManager;

        [SetUp]
        public void SetUp()
        {
            robot = new Robot("Robi", 20);
            robotManager = new RobotManager(3);
        }

        [Test]
        public void Test_RobotConstuctorSetsValuesProperly()
        {
            Assert.AreEqual("Robi", robot.Name);
            Assert.AreEqual(20, robot.Battery);
            Assert.AreEqual(20, robot.MaximumBattery);
        }

        [Test]
        public void Test_RobotManagerConstructorSetsValuesProperly()
        {
            Assert.AreEqual(3, robotManager.Capacity);
            Assert.AreEqual(0, robotManager.Count);
        }

        [TestCase(-1)]
        [TestCase(-10)]
        public void Test_RobotCapacityThrowsWhenNegativeNumber(int value)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                robotManager = new RobotManager(value);
            });
        }

        [Test]
        public void Test_AddRobotShouldThrowWithDuplicateRobot()
        {
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(robot);
            });
        }

        [Test]
        public void Test_AddRobotShouldThrowWithDuplicateNameRobot()
        {
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(new Robot("Robi", 30));
            });
        }

        [Test]
        public void Test_AddRobotThrowsWhenCapacityReached()
        {
            robotManager.Add(robot);
            robotManager.Add(new Robot("Gog", 10));
            robotManager.Add(new Robot("Apple", 40));

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(new Robot("Tesla", 100));
            });
        }

        [Test]
        public void Test_AddRobotAddsRobotToTheCollection()
        {
            robotManager.Add(robot);

            Assert.AreEqual(1, robotManager.Count);
        }

        [Test]
        public void Test_RemoveRobotThrowsWhenRobotNotExisting()
        {
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Remove("RandomName");
            });
        }

        [Test]
        public void Test_RemoveRobotRemovesTheRobotFromTheCollection()
        {
            robotManager.Add(robot);

            robotManager.Remove("Robi");

            Assert.AreEqual(0, robotManager.Count);
        }

        [Test]
        public void Test_WorkThrowsWhenRobotNotExisting()
        {
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Work("RandomName", "RandomJob", 5);
            });
        }

        [Test]
        public void Test_WorkThrowsWhenNotEnoughBatter()
        {
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Work("Robi", "RandomJob", 30);
            });
        }

        [Test]
        public void Test_WorkDecreasesRobotBattery()
        {
            robotManager.Add(robot);

            robotManager.Work("Robi", "RandomJob", 15);

            Assert.AreEqual(5, robot.Battery);
        }

        [Test]
        public void Test_ChargeThrowsWhenRobotNotExisting()
        {
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Charge("RandomName");
            });
        }

        [Test]
        public void Test_ChargeSetsRobotBatteryToMaximum()
        {
            robotManager.Add(robot);

            robotManager.Work("Robi", "RandomJob", 15);

            robotManager.Charge("Robi");

            Assert.AreEqual(20, robot.Battery);
        }
    }
}
