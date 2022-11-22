namespace BookingApp.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Models.Rooms.Contracts;
    using Contracts;
   

    public class RoomRepository : IRepository<IRoom>
    {
        private List<IRoom> rooms;

        public RoomRepository()
        {
            rooms= new List<IRoom>();
        }

        public void AddNew(IRoom model)
        {
            rooms.Add(model);
        }

        public IReadOnlyCollection<IRoom> All()
        {
            return rooms.AsReadOnly();
        }

        public IRoom Select(string criteria)
        {
            return rooms.FirstOrDefault(r => r.GetType().Name == criteria);
        }
    }
}
