using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {

            [Test]
            public void Test_CarConstructorSetValuesProperly()
            {
                string carModel = "BMW";
                int numberOfIssues = 4;

                var car = new Car(carModel, numberOfIssues);

                Assert.AreEqual(numberOfIssues, car.NumberOfIssues);
                Assert.AreEqual(carModel, car.CarModel);
            }

            [Test]
            public void Test_CarIsFixedReturnsTrueWhenCarHas0Issues()
            {
                int numberOfIssues = 0;
               
                var car = new Car("BMW", numberOfIssues);

                Assert.IsTrue(car.IsFixed);
            }

            [Test]
            public void Test_CarIsFixedReturnsFalseWhenCarHasIssues()
            {
                int numberOfIssues = 2;

                var car = new Car("BMW", numberOfIssues);

                Assert.IsFalse(car.IsFixed);
            }

            [Test]
            public void Test_GarageConstructorSetValuesProperly()
            {
                string name = "WestCoast";
                int mechanicsAvaliable = 3;

                var garage = new Garage(name, mechanicsAvaliable);

                Assert.AreEqual(name, garage.Name);
                Assert.AreEqual(mechanicsAvaliable, garage.MechanicsAvailable);
            }

            [TestCase("West")]
            [TestCase("Coast")]
            [TestCase("C")]
            public void Test_NameSetsValueProperly(string name)
            {
                int mechanicsAvaliable = 3;
                var garage = new Garage(name, mechanicsAvaliable);

                Assert.AreEqual(name, garage.Name);
            }

            [TestCase("")]
            [TestCase(null)]
            public void Test_NameShouldThrowWithInvalidInput(string name)
            {
                int mechanicsAvaliable = 3;


                Assert.Throws<ArgumentNullException>(() =>
                {
                    var garage = new Garage(name, mechanicsAvaliable);
                }, "Invalid garage name.");
            }


            [TestCase(0)]
            [TestCase(-1)]
            public void Test_MechanicsAvaliableShouldThrowWithZeroOrNegativeInput(int mechanics)
            {
               
                Assert.Throws<ArgumentException>(() =>
                {
                    var garage = new Garage("West", mechanics);
                }, "At least one mechanic must work in the garage.");
            }

            [Test]
            public void Test_CarsInGarageReturnsCorrectCount()
            {
                int mechanics = 3;
                var garage = new Garage("West", mechanics);

                garage.AddCar(new Car("BMW", 2));

                int expectedCount = 1;
                Assert.AreEqual(expectedCount, garage.CarsInGarage);
            }

            [Test]
            public void Test_AddCarAddCarToTheDataProperly()
            {
                int mechanics = 3;
                var garage = new Garage("West", mechanics);

                garage.AddCar(new Car("BMW", 2));

                int expectedCount = 1;
                Assert.AreEqual(expectedCount, garage.CarsInGarage);
            }


            [Test]
            public void Test_AddCarThrowsWhenNoAvaliableMechanics()
            {
                int mechanics = 3;
                var garage = new Garage("West", mechanics);

                garage.AddCar(new Car("BMW", 2));
                garage.AddCar(new Car("BMW", 2));
                garage.AddCar(new Car("BMW", 2));

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.AddCar(new Car("BMW", 2));
                }, "No mechanic available.");
            }

            [Test]
            public void Test_FixCarFindsAndSetsCarNumberOfIssuesTo0()
            {
                int mechanics = 3;
                var garage = new Garage("West", mechanics);

                garage.AddCar(new Car("BMW", 2));
                garage.AddCar(new Car("Scoda", 2));
                garage.AddCar(new Car("VW", 2));

                var fixedCar = garage.FixCar("BMW");

                string expectedCarModel = "BMW";
                int expectedNumberOfIssues = 0;

                Assert.AreEqual(expectedCarModel, fixedCar.CarModel);
                Assert.AreEqual(expectedNumberOfIssues, fixedCar.NumberOfIssues);
            }

            [Test]
            public void Test_FixCarThrowsWhenCarNotFound()
            {
                int mechanics = 3;
                var garage = new Garage("West", mechanics);

                garage.AddCar(new Car("BMW", 2));
                garage.AddCar(new Car("Scoda", 2));
                garage.AddCar(new Car("VW", 2));

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.FixCar("Mercedes");
                }, "The car carModel doesn't exist.");
            }

            [Test]
            public void Test_RemoveFixedCarRemovesFixedCars()
            {
                int mechanics = 3;
                var garage = new Garage("West", mechanics);

                garage.AddCar(new Car("BMW", 2));
                garage.AddCar(new Car("Scoda", 2));
                garage.AddCar(new Car("VW", 2));

                garage.FixCar("BMW");

                garage.RemoveFixedCar();

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.FixCar("BMW");
                });
            }

            [Test]
            public void Test_RemoveFixedCarsThrowsWhenNoFixedCars()
            {
                int mechanics = 3;
                var garage = new Garage("West", mechanics);

                garage.AddCar(new Car("BMW", 2));
                garage.AddCar(new Car("Scoda", 2));
                garage.AddCar(new Car("VW", 2));

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.RemoveFixedCar();
                }, "No fixed cars available.");
            }

            [Test]
            public void Test_ReportReturnProperOutput()
            {

                int mechanics = 3;
                var garage = new Garage("West", mechanics);

                garage.AddCar(new Car("BMW", 2));
                garage.AddCar(new Car("Scoda", 2));
                garage.AddCar(new Car("VW", 2));

                string expectedOutput = "There are 3 which are not fixed: BMW, Scoda, VW.";

                string output = garage.Report();

                Assert.AreEqual(expectedOutput, output);
            }
        }
    }
}