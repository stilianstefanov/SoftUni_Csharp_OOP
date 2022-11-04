namespace MilitaryElite.Models.Contracts
{
    using System.Collections.Generic;

    public interface ICommando : ISpecialisedSoldier
    {
        public IReadOnlyCollection<Mission> Missions { get; }
    }
}
