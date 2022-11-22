namespace BookingApp.Models.Bookings
{
    using System;
    using System.Text;

    using Contracts;
    using Utilities.Messages;   
    using Rooms.Contracts;
    

    public class Booking : IBooking
    {
        private IRoom room;
        private int residenceDuration;
        private int adultsCount;
        private int childrenCount;
        private int bookingNumber;

        public Booking(IRoom room, int residenceDuration, int adultsCount, int childrenCount, int bookingNumber)
        {
            this.Room = room;
            this.ResidenceDuration = residenceDuration;
            this.AdultsCount= adultsCount;
            this.ChildrenCount= childrenCount;
            this.BookingNumber= bookingNumber;
        }

        public IRoom Room
        {
            get
            {
                return room;
            }
            private set
            {
                room = value;
            }
        }

        public int ResidenceDuration
        {
            get
            {
                return residenceDuration;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.DurationZeroOrLess);
                }
                residenceDuration = value;
            }
        }

        public int AdultsCount 
        {
            get
            {
                return adultsCount;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.AdultsZeroOrLess);
                }
                adultsCount = value;
            }
        }

        public int ChildrenCount
        {
            get
            {
                return childrenCount;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.ChildrenNegative);
                }
                childrenCount = value;
            }
        }

        public int BookingNumber
        {
            get
            {
                return bookingNumber;
            }
            private set
            {
                bookingNumber = value;
            }
        }

        public string BookingSummary()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Booking number: {BookingNumber}");
            sb.AppendLine($"Room type: {Room.GetType().Name}");
            sb.AppendLine($"Adults: {AdultsCount} Children: {ChildrenCount}");
            sb.Append($"Total amount paid: {TotalPaid():F2} $");

            return sb.ToString().TrimEnd();
        }

        private double TotalPaid()
            => Math.Round(ResidenceDuration * Room.PricePerNight, 2);
    }
}
