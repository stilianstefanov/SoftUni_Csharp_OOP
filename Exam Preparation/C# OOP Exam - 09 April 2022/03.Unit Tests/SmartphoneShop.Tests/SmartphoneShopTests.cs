using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {

        [Test]
        public void Test_ConstructorSetsValuesProperly()
        {
            int capacity = 5;

            var shop = new Shop(capacity);

            Assert.AreEqual(capacity, shop.Capacity);
        }

        [TestCase(-1)]
        [TestCase(-10)]
        public void Test_CapacityShouldThrowWithNegativeValue(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var shop = new Shop(capacity);
            }, "Invalid capacity.");
        }

        [Test]
        public void Test_CountReturnsValuesProperly()
        {
            int capacity = 5;
            var shop = new Shop(capacity);

            shop.Add(new Smartphone("Iphone", 100));
            shop.Add(new Smartphone("Samsung", 100));

            int expectedCount = 2;

            Assert.AreEqual(expectedCount, shop.Count);
        }

        [Test]
        public void Test_AddThrowsWithDuplicatePhoneName()
        {
            int capacity = 5;
            var shop = new Shop(capacity);

            shop.Add(new Smartphone("Iphone", 100));
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(new Smartphone("Iphone", 100));
            });
        }

        [Test]
        public void Test_AddThrowsWhenShopIsFull()
        {
            int capacity = 1;

            var shop = new Shop(capacity);

            shop.Add(new Smartphone("Iphone", 100));
            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(new Smartphone("Samsung", 100));
            });
        }

        [Test]
        public void Test_RemoveThrowsWhenNoPhoneFound()
        {
            int capacity = 5;

            var shop = new Shop(capacity);

            shop.Add(new Smartphone("Iphone", 100));
            shop.Add(new Smartphone("Samsung", 100));

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Remove("Xiaomi");
            });
        }

        [Test]
        public void Test_RemoveDecreasesCountProperly()
        {
            int capacity = 5;

            var shop = new Shop(capacity);

            shop.Add(new Smartphone("Iphone", 100));
            shop.Add(new Smartphone("Samsung", 100));

            shop.Remove("Samsung");

            int expectedCount = 1;
            Assert.AreEqual(expectedCount, shop.Count);          
        }

        [Test]
        public void Test_TestPhoneThrowsWhenPhoneNotFound()
        {
            int capacity = 5;
            int batteryUsage = 30;

            var shop = new Shop(capacity);

            shop.Add(new Smartphone("Iphone", 100));
            shop.Add(new Smartphone("Samsung", 100));

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("Xiaomi", batteryUsage);
            });
        }

        [Test]
        public void Test_TestPhoneShouldThrowWhenBatteryTooLow()
        {
            int capacity = 5;
            int batteryUsage = 30;

            var shop = new Shop(capacity);

            shop.Add(new Smartphone("Iphone", 20));
            shop.Add(new Smartphone("Samsung", 100));

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("Iphone", batteryUsage);
            });
        }

        [Test]
        public void Test_TestPhoneReducesSmartPhoneBattery()
        {

            int capacity = 5;
            int batteryUsage = 30;

            var shop = new Shop(capacity);
            var iPhone = new Smartphone("Iphone", 100);

            shop.Add(iPhone);
            shop.Add(new Smartphone("Samsung", 100));

            int expectedBatteryLeft = 70;
            shop.TestPhone("Iphone", batteryUsage);

            Assert.AreEqual(expectedBatteryLeft, iPhone.CurrentBateryCharge);
        }

        [Test]
        public void Test_ChargePhoneThrowsWhenPhoneNotFound()
        {
            int capacity = 5;

            var shop = new Shop(capacity);

            shop.Add(new Smartphone("Iphone", 100));
            shop.Add(new Smartphone("Samsung", 100));

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.ChargePhone("Xiaomi");
            });
        }

        [Test]
        public void Test_ChargePhoneIncreasesPhoneBatteryToMax()
        {

            int capacity = 5;

            var shop = new Shop(capacity);
            var iPhone = new Smartphone("Iphone", 100);

            shop.Add(iPhone);
            shop.Add(new Smartphone("Samsung", 100));

            int expectedBattery = iPhone.MaximumBatteryCharge;
            shop.TestPhone("Iphone", 30);
            shop.ChargePhone("Iphone");

            Assert.AreEqual(expectedBattery, iPhone.CurrentBateryCharge);
        }
    }
}