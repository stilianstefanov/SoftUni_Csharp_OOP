
namespace BookingApp.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Models.Bookings.Contracts;
    using Repositories.Contracts;
   

    public class BookingRepository : IRepository<IBooking>
    {
        private List<IBooking> bookings;

        public BookingRepository()
        {
            bookings= new List<IBooking>();
        }
        public void AddNew(IBooking model)
        {
            bookings.Add(model);
        }

        public IReadOnlyCollection<IBooking> All()
        {
            return bookings.AsReadOnly();
        }

        public IBooking Select(string criteria)
        {
            int bookingNumber = int.Parse(criteria);

            return bookings.FirstOrDefault(b => b.BookingNumber == bookingNumber);
        }
    }
}
