namespace Formula1.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Contracts;
    

    public class RaceRepository : IRepository<IRace>
    {
        private List<IRace> races;

        public RaceRepository()
        {
            races = new List<IRace>();
        }

        public IReadOnlyCollection<IRace> Models => races.AsReadOnly();

        public void Add(IRace model)
        {
            races.Add(model);
        }

        public IRace FindByName(string name)
        {
            return races.FirstOrDefault(r => r.RaceName == name);
        }

        public bool Remove(IRace model)
        {
            return races.Remove(model);
        }
    }
}
