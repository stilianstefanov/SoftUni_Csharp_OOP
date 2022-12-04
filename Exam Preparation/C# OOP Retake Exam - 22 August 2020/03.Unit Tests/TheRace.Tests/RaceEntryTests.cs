using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private UnitDriver unitDriver;
        private UnitCar unitCar;
        private RaceEntry raceEntry;

        [SetUp]
        public void Setup()
        {
            unitCar = new UnitCar("BMW", 300, 3000);
            unitDriver = new UnitDriver("Max", unitCar);
            raceEntry = new RaceEntry();
        }

        [Test]
        public void Test_UnitCarConstructorSetsValuesProperly()
        {
            Assert.AreEqual("BMW", unitCar.Model);
            Assert.AreEqual(300, unitCar.HorsePower);
            Assert.AreEqual(3000, unitCar.CubicCentimeters);
        }

        [Test]
        public void Test_UnitDriverConstructorSetsValuesProperly()
        {
            Assert.AreEqual("Max", unitDriver.Name);
            Assert.AreEqual(unitCar, unitDriver.Car);
            Assert.AreEqual("BMW", unitDriver.Car.Model);
        }

        [Test]
        public void Test_UnitDriverThrowsWithNullName()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                unitDriver = new UnitDriver(null, unitCar);
            }, "Name cannot be null!");
        }

        [Test]
        public void Test_RaceEntryInitializesTheCollection()
        {
            Assert.AreEqual(0, raceEntry.Counter);
        }

        [Test]
        public void Test_AddDriverThrowsWithNullDriver()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                raceEntry.AddDriver(null);
            }, "Driver cannot be null.");
        }

        [Test]
        public void Test_AddDriverThrowsWithDuplicateDriver()
        {
            raceEntry.AddDriver(unitDriver);
            Assert.Throws<InvalidOperationException>(() =>
            {
                raceEntry.AddDriver(unitDriver);
            });
        }


        [Test]
        public void Test_AddDriverThrowsWithDuplicateNameDriver()
        {
            raceEntry.AddDriver(unitDriver);
            UnitDriver unitDriver2 = new UnitDriver("Max", new UnitCar("Alpha", 150, 2000));

            Assert.Throws<InvalidOperationException>(() =>
            {
                raceEntry.AddDriver(unitDriver2);
            });
        }

        [Test]
        public void Test_AddDriverAddsNewDriverProperly()
        {
            string expectedOutput = "Driver Max added in race.";

            string output = raceEntry.AddDriver(unitDriver);

            Assert.AreEqual(expectedOutput, output);
            Assert.AreEqual(1, raceEntry.Counter);
        }

        [Test]
        public void Test_CalculateAvarageHorsePowerThrowsWithLessThanMinimumDrivers()
        {
            raceEntry.AddDriver(unitDriver);

            Assert.Throws<InvalidOperationException>(() =>
            {
                raceEntry.CalculateAverageHorsePower();
            }, "The race cannot start with less than 2 participants.");
        }

        [Test]
        public void Test_CalculateAvarageHorsePowerReturnsValidValue()
        {
            raceEntry.AddDriver(unitDriver);
            UnitDriver unitDriver2 = new UnitDriver("Lewis", new UnitCar("Merc", 160, 2500));
            raceEntry.AddDriver(unitDriver2);
            List<UnitDriver> drivers = new List<UnitDriver> { unitDriver, unitDriver2 };

            double expectedOutput = drivers.Select(d => d.Car.HorsePower).Average();

            double output = raceEntry.CalculateAverageHorsePower();

            Assert.AreEqual(expectedOutput, output);
        }
    }
}