using NUnit.Framework;
using System;
using System.Linq;

namespace Computers.Tests
{
    public class Tests
    {
        private Computer computer;
        private ComputerManager manager;

        [SetUp]
        public void Setup()
        {
            computer = new Computer("Asus", "Vivo", 700);
            manager = new ComputerManager();
        }

        [Test]
        public void TestComputerConstructorSetsValuesProperly()
        {
            Assert.AreEqual("Asus", computer.Manufacturer);
            Assert.AreEqual("Vivo", computer.Model);
            Assert.AreEqual(700, computer.Price);
        }

        [Test]
        public void Test_ManagerInitilizesTheCollection()
        {
            Assert.IsNotNull(manager.Computers);
            Assert.AreEqual(0, manager.Count);
        }

        [Test]
        public void Test_AddComputerThrowsWithNullValue()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                manager.AddComputer(null);
            });
        }

        [Test]
        public void Test_AddComputerThrowsWithDuplicateComputer()
        {
            manager.AddComputer(computer);
            Assert.Throws<ArgumentException>(() =>
            {
                manager.AddComputer(computer);
            });
        }

        [Test]
        public void Test_AddComputerThrowsWithDuplicateManAndModelComputer()
        {
            manager.AddComputer(computer);
            var computer2 = new Computer("Asus", "Vivo", 500);
            Assert.Throws<ArgumentException>(() =>
            {
                manager.AddComputer(computer2);
            });
        }

        [Test]
        public void Test_AddComputerAddsComputerProperly()
        {
            manager.AddComputer(computer);
            var computers = manager.Computers;

            Assert.AreEqual(1, manager.Count);
            Assert.IsTrue(computers.Contains(computer));
        }

        [TestCase(null, "Vivo")]
        [TestCase("Asus", null)]
        public void Test_RemoveComputerThrowsWithNullParams(string manufacturer, string model)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                manager.RemoveComputer(manufacturer, model);
            });
        }

        [Test]
        public void Test_RemoveComputerThrowsWithNotExistantComputer()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                manager.RemoveComputer("HP", "randomModel");
            });
        }

        [Test]
        public void Test_RemoveComputerRemovesTheComputerFromTheCollection()
        {
            manager.AddComputer(computer);

            var outputComputer = manager.RemoveComputer("Asus", "Vivo");

            Assert.AreEqual(0, manager.Count);
            Assert.IsFalse(manager.Computers.Contains(computer));
        }

        [Test]
        public void Test_RemoveComputerReturnsTheCorrectComputer()
        {
            manager.AddComputer(computer);

            var outputComputer = manager.RemoveComputer("Asus", "Vivo");

            Assert.AreEqual(computer, outputComputer);
        }

        [Test]
        public void Test_GetComputerReturnsTheCorrectComputer()
        {
            manager.AddComputer(computer);

            var outputComputer = manager.GetComputer("Asus", "Vivo");

            Assert.AreEqual(computer, outputComputer);
        }


        [TestCase(null, "Vivo")]
        [TestCase("Asus", null)]
        public void Test_GetComputerThrowsWithNullParams(string manufacturer, string model)
        {

            Assert.Throws<ArgumentNullException>(() =>
            {
                manager.GetComputer(manufacturer, model);
            });
        }

        [Test]
        public void Test_GetComputerThrowsWithNotExistantComputer()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                manager.GetComputer("HP", "randomModel");
            });
        }

        [Test]
        public void Test_GetComputersByManufacturerThrowsWithNullInput()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                manager.GetComputersByManufacturer(null);
            });
        }

        [Test]
        public void Test_GetComputersByManufacturerReturnsTheProperComputers()
        {
            manager.AddComputer(computer);
            var computer2 = new Computer("Asus", "Predator", 1000);
            var computer3 = new Computer("Hp", "someModel", 800);

            manager.AddComputer(computer2);
            manager.AddComputer(computer3);

            var outputCollection = manager.GetComputersByManufacturer("Asus");

            Assert.IsTrue(outputCollection.Contains(computer));
            Assert.IsTrue(outputCollection.Contains(computer2));
            Assert.IsFalse(outputCollection.Contains(computer3));
            Assert.AreEqual(2, outputCollection.Count);
        }
    }
}