using FrontDeskApp;
using NUnit.Framework;
using System;

namespace BookigApp.Tests
{
    
    public class Tests
    {
        private Hotel hotel;
        private string fullName;
        private int category;

        private const int adults = 2;
        private const int children = 2;
        private const int duration = 3;
        private const double budget = 400.00;

        [SetUp]
        public void Setup()
        {
            fullName = "Cherno More";
            category = 4;
            hotel = new Hotel(fullName, category);
        }

        [Test]
        public void Test1_ConstructorWorksProperly()
        {
            Assert.AreEqual(fullName, hotel.FullName);
            Assert.AreEqual(category, hotel.Category);
            
            Assert.True(hotel.Bookings != null);
            Assert.True(hotel.Rooms != null);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Test2_FullNameThrowsWhenNullOrWhiteSpace(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                hotel = new Hotel(name, category);
            }, "FullName cannot be null, empty or whitespace!");                       
        }

        [TestCase(0)]
        [TestCase(6)]
        public void Test3_CategoryThrowsWhenInvalidInput(int category)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                hotel = new Hotel(fullName, category);
            }, "Category should be between 1 and 5 starts");
        }

        [Test]
        public void Test4_AddRoomAddsRoomProperly()
        {
            int expectedRoomsCount = 1;

            hotel.AddRoom(new Room(6, 10.5));

            Assert.AreEqual(expectedRoomsCount, hotel.Rooms.Count);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test5_BookRoomThrowsWhenAdultsNotPositive(int adults)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(adults, children, duration, budget);
            });
        }
       
        [TestCase(-1)]
        public void Test6_BookRoomThrowsWhenChildrenNegative(int children)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(adults, children, duration, budget);
            });
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test6_BookRoomThrowsWhenDurationNotPositive(int duration)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(adults, children, duration, budget);
            });
        }

        [Test]
        public void Test7_BookRoomTurnoverCalculatesProperly()
        {
            hotel.AddRoom(new Room(1, 30));
            hotel.AddRoom(new Room(2, 40));
            hotel.AddRoom(new Room(3, 50));
            hotel.AddRoom(new Room(4, 60));

            hotel.BookRoom(adults, children, duration, budget);

            int expectedTurnOver = duration * 60;

            Assert.AreEqual(expectedTurnOver, hotel.Turnover);
        }

        [Test]
        public void Test8_BookRoomAddsBookingToTheCollection()
        {
            hotel.AddRoom(new Room(4, 60));

            hotel.BookRoom(adults, children, duration, budget);

            int expectedBookingsCount = 1;

            Assert.AreEqual(expectedBookingsCount, hotel.Bookings.Count);
        }
    }
}