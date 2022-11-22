namespace BookingApp.Models.Hotels
{
    using System;

    using Bookings.Contracts;
    using Rooms.Contracts;
    using Repositories.Contracts;
    using Contacts;    
    using Utilities.Messages;
    using Repositories;

    public class Hotel : IHotel
    {
        private string fullName;
        private int category;
        private IRepository<IRoom> rooms;
        private IRepository<IBooking> bookings;

        public Hotel(string fullName, int category)
        {
            this.FullName= fullName;
            this.Category= category;
            rooms = new RoomRepository();
            bookings= new BookingRepository();
        }
        public string FullName
        {
            get
            {
                return fullName;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HotelNameNullOrEmpty);
                }
                fullName = value;
            }
        }

        public int Category
        {
            get
            {
                return category;
            }
            private set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCategory);
                }
                category = value;
            }
        }

        public double Turnover
        {
            get
            {
                return GetTurnOver();
            }
        }

        public IRepository<IRoom> Rooms => rooms;

        public IRepository<IBooking> Bookings => bookings;

        private double GetTurnOver()
        {
            double turnOver = 0;
            var bookingsRead = Bookings.All();

            foreach (var booking in bookingsRead)
            {
                turnOver += booking.ResidenceDuration * booking.Room.PricePerNight;
            }

            return Math.Round(turnOver, 2);
        }
    }
}
