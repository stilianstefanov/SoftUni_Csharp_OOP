
namespace BookingApp.Repositories
{
    using Models.Hotels.Contacts;
    using Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class HotelRepository : IRepository<IHotel>
    {
        private List<IHotel> hotels;

        public HotelRepository()
        {
            hotels = new List<IHotel>();
        }

        public void AddNew(IHotel model)
        {
            hotels.Add(model);
        }

        public IReadOnlyCollection<IHotel> All()
        {
            return hotels.AsReadOnly();
        }

        public IHotel Select(string criteria)
        {
            return hotels.FirstOrDefault(h => h.FullName == criteria);
        }
    }
}
