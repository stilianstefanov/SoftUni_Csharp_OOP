namespace Gym.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Equipment.Contracts;
    

    public class EquipmentRepository : IRepository<IEquipment>
    {
        private List<IEquipment> equipment;

        public EquipmentRepository()
        {
            this.equipment= new List<IEquipment>();
        }

        public IReadOnlyCollection<IEquipment> Models => equipment.AsReadOnly();

        public void Add(IEquipment model)
        {
            equipment.Add(model);
        }

        public IEquipment FindByType(string type)
        {
            return equipment.FirstOrDefault(e => e.GetType().Name == type);
        }

        public bool Remove(IEquipment model)
        {
            return equipment.Remove(model);
        }
    }
}
