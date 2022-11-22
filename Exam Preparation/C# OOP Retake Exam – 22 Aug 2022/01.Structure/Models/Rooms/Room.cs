namespace BookingApp.Models.Rooms
{
    using System;

    using Utilities.Messages;
    using Contracts;
    

    public abstract class Room : IRoom
    {
        private int bedCapacity;
        private double pricePerNight;

        protected Room(int bedCapacity)
        {
            this.BedCapacity = bedCapacity;
            this.PricePerNight = 0;
        }
        public int BedCapacity
        {
            get
            {
                return bedCapacity;
            }
            private set
            {
                bedCapacity = value;
            }
        }

        public double PricePerNight
        {
            get
            {
                return pricePerNight;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.PricePerNightNegative);
                }
                pricePerNight = value;
            }
        }

        public void SetPrice(double price)
            => this.PricePerNight = price;
        
    }
}
