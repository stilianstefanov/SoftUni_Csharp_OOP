namespace AquaShop.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Models.Decorations.Contracts;
    using Contracts;
   

    public class DecorationRepository : IRepository<IDecoration>
    {
        private List<IDecoration> models;

        public DecorationRepository()
        {
            models= new List<IDecoration>();
        }

        public IReadOnlyCollection<IDecoration> Models => models.AsReadOnly();

        public void Add(IDecoration model)
        {
            models.Add(model);
        }

        public IDecoration FindByType(string type)
        {
            return models.FirstOrDefault(d => d.GetType().Name == type);
        }

        public bool Remove(IDecoration model)
        {
            return models.Remove(model);
        }
    }
}
