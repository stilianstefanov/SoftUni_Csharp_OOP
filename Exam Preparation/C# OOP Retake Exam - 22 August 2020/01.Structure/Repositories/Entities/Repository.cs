namespace EasterRaces.Repositories.Entities
{
    using System.Collections.Generic;

    using Contracts;

    public abstract class Repository<T> : IRepository<T>
    {
        private List<T> models;

        public Repository()
        {
            models = new List<T>();
        }

        public void Add(T model)
        {
            models.Add(model);
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return models.AsReadOnly();
        }

        public abstract T GetByName(string name);
   
        public bool Remove(T model)
        {
            return models.Remove(model);
        }
    }
}
