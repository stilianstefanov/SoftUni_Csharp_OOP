namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;   

    [TestFixture]
    public class CarManagerTests
    {
        private const string MAKE = "bmw";
        private const string MODEL = "330d";
        private const double FUEL_CONSUMPTION = 6.0;
        private const double FUEL_AMOUNT = 30.0;
        private const double FUEL_CAPACITY = 60.0;
        private Car car;

        [SetUp]
        public void SetUp()
        {
            car = new Car(MAKE, MODEL, FUEL_CONSUMPTION, FUEL_CAPACITY);
        }

        [Test]
        public void Test_ConstuctorWorksProperly()
        {
            Assert.AreEqual(MAKE, car.Make);
            Assert.AreEqual(MODEL, car.Model);
            Assert.AreEqual(FUEL_CONSUMPTION, car.FuelConsumption);
            Assert.AreEqual(FUEL_CAPACITY, car.FuelCapacity);
            Assert.AreEqual(0, car.FuelAmount);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Test_InitializeEntityWithEmptyMakeShouldThrow(string make)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(make, MODEL, FUEL_CONSUMPTION, FUEL_CAPACITY);
            }, "Should not be initialized with empty input for Make");
        }

        [TestCase("")]
        [TestCase(null)]
        public void Test_InitializeEntityWithEmptyModelShouldThrow(string model)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(MAKE, model, FUEL_CONSUMPTION, FUEL_CAPACITY);
            }, "Should not be initialized with empty input for Model");
        }

        [TestCase(-1.0)]
        [TestCase(0.0)]
        public void Test_InitializeEntityWithNegativeFuelConsShouldThrow(double fuelCons)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(MAKE, MODEL, fuelCons, FUEL_CAPACITY);
            }, "Should not be initialized with negative value for Fuel Consumption");
        }

        [Test]
        public void Test_FuelAmmountCanNotBeNegativeShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                car.Drive(20);
            });
        }

        [TestCase(-1.0)]
        [TestCase(0.0)]
        public void Test_FuelCapacityCanNotBeNegativeShouldThrow(double fuelCap)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(MAKE, MODEL, FUEL_CONSUMPTION, fuelCap);
            }, "Should not be initialized with negative value for Fuel Capacity");
        }

        [Test]
        public void Test_RefuelAddsFuelProperly()
        {
            double fuelToAdd = 20;
            double expectedFuel = 20;

            car.Refuel(fuelToAdd);

            Assert.AreEqual(expectedFuel, car.FuelAmount, "Refuel does not adding fuel properly");
        }

        [TestCase(-1)]
        [TestCase(0.0)]
        public void Test_RefuelCannotAcceptNegativeValueShouldThrow(double fuelToAdd)
        {
            Assert.Throws<ArgumentException>(() =>
            {
             car.Refuel(fuelToAdd);
            }, "Should not be refueled with negative value");
        }

        [Test]
        public void Test_RefuelShouldNotExceedFuelCapacity()
        {
            double fuelToAdd = 65.0;
            double expectedFuelAmmount = car.FuelCapacity;

            car.Refuel(fuelToAdd);

            Assert.AreEqual(expectedFuelAmmount, car.FuelAmount, "Fuel Ammount should not exceed the capacity");
        }

        [TestCase(100)]
        [TestCase(65.5)]
        public void Test_DriveShouldDecreaseFuelAmmountProperly(double distanceToDrive)
        {
            car.Refuel(60.0);

            double expectedFuelAmmountLeft = car.FuelAmount - (distanceToDrive / 100) * car.FuelConsumption;

            car.Drive(distanceToDrive);

            Assert.AreEqual(expectedFuelAmmountLeft, car.FuelAmount, "Drive does not decrease Fuel Ammount properly");
        }

        [Test]
        public void Test_DriveShouldThrowWhenNotEnoughFuel()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                car.Drive(500);
            }, "Drive should throw when not enough fuel for the distance");
        }
    }
}