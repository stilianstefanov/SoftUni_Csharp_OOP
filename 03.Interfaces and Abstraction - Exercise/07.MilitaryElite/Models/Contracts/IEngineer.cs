namespace MilitaryElite.Models.Contracts
{
    using System.Collections.Generic;

    public interface IEngineer : ISpecialisedSoldier
    {
        public IReadOnlyCollection<Repair> Repairs { get; }
    }
}
