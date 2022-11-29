namespace CarRacing.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Utilities.Messages;
    using Models.Racers.Contracts;
    using Repositories.Contracts;


    public class RacerRepository : IRepository<IRacer>
    {
        private List<IRacer> racers;

        public RacerRepository()
        {
            racers = new List<IRacer>();
        }

        public IReadOnlyCollection<IRacer> Models => racers.AsReadOnly();

        public void Add(IRacer model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddRacerRepository);
            }
            racers.Add(model);
        }

        public IRacer FindBy(string property)
        {
            return racers.FirstOrDefault(r => r.Username == property);
        }

        public bool Remove(IRacer model)
        {
            return racers.Remove(model);
        }
    }
}
