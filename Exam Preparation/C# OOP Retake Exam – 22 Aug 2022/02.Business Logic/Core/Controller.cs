namespace BookingApp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.Bookings;
    using Models.Hotels;
    using Models.Hotels.Contacts;
    using Models.Rooms;
    using Models.Rooms.Contracts;
    using Repositories;
    using Utilities.Messages;
    
   

    public class Controller : IController
    {
        private HotelRepository hotels;

        public Controller()
        {
            hotels= new HotelRepository();
        }

        public string AddHotel(string hotelName, int category)
        {
            if (hotels.All().Any(h => h.FullName == hotelName))
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }

            hotels.AddNew(new Hotel(hotelName, category));

            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (!hotels.All().Any(h => h.Category == category))
            {
                return string.Format(OutputMessages.CategoryInvalid, category);
            }
            var orderedHotels = hotels.All().Where(h => h.Category == category)
                .OrderBy(h => h.FullName);

            List<Tuple<string, IRoom>> allRoomsWithPrices = new List<Tuple<string, IRoom>>();

            foreach (var hotel in orderedHotels)
            {
                foreach (var room in hotel.Rooms.All())
                {
                    if (room.PricePerNight > 0)
                    {
                        allRoomsWithPrices.Add(new Tuple<string, IRoom>(hotel.FullName, room));
                    }
                }
            }

            allRoomsWithPrices = allRoomsWithPrices.OrderBy(r => r.Item2.BedCapacity).ToList();

            string hotelName = string.Empty;
            IRoom roomToBook = null;

            int totalGuests = adults + children;

            foreach (var roomTuple in allRoomsWithPrices)
            {
                if (roomTuple.Item2.BedCapacity >= totalGuests)
                {
                    roomToBook = roomTuple.Item2;
                    hotelName = roomTuple.Item1;
                    break;
                }
            }

            if (roomToBook == null)
            {
                return string.Format(OutputMessages.RoomNotAppropriate);
            }

            IHotel hotelToBook = orderedHotels.First(h => h.FullName== hotelName);

            int bookingNumber = hotelToBook.Bookings.All().Count() + 1;

            hotelToBook.Bookings.AddNew(new Booking(roomToBook, duration, adults, children, bookingNumber));

            return string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotelName);
        }

        public string HotelReport(string hotelName)
        {
            if (!hotels.All().Any(h => h.FullName == hotelName))
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            IHotel hotel = hotels.Select(hotelName);

            var sb = new StringBuilder();
            sb.AppendLine($"Hotel name: {hotelName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            sb.AppendLine("--Bookings:");
            sb.AppendLine();
            if (!hotel.Bookings.All().Any())
            {
                sb.AppendLine("none");
            }
            else
            {
                foreach (var booking in hotel.Bookings.All())
                {
                    sb.AppendLine(booking.BookingSummary());
                    sb.AppendLine();
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            if (!hotels.All().Any(h => h.FullName == hotelName))
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            IHotel hotel = hotels.Select(hotelName);
            if (roomTypeName != "Apartment" && roomTypeName != "DoubleBed" && roomTypeName != "Studio")
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            if (hotel.Rooms.Select(roomTypeName) == null)
            {
                return string.Format(OutputMessages.RoomTypeNotCreated);
            }

            IRoom roomToSetPrice = hotel.Rooms.Select(roomTypeName);
            if (roomToSetPrice.PricePerNight != 0)
            {
                throw new InvalidOperationException(ExceptionMessages.PriceAlreadySet);
            }

            roomToSetPrice.SetPrice(price);

            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            if (!hotels.All().Any(h => h.FullName == hotelName))
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            IHotel hotel = hotels.Select(hotelName);
            if (hotel.Rooms.Select(roomTypeName) != null)
            {
                return string.Format(OutputMessages.RoomTypeAlreadyCreated);
            }

            if (roomTypeName != "Apartment" && roomTypeName != "DoubleBed" && roomTypeName != "Studio")
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            IRoom roomToAdd = null;
            switch (roomTypeName)
            {
                case "Apartment":
                    roomToAdd = new Apartment();
                    break;
                case "DoubleBed":
                    roomToAdd = new DoubleBed();
                    break;
                case "Studio":
                    roomToAdd = new Studio();
                    break;               
            }

            hotel.Rooms.AddNew(roomToAdd);

            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }
    }
}
