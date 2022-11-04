namespace MilitaryElite.Models.Contracts
{
    public interface ISpecialisedSoldier : IPrivate
    {
        public string Corps { get; }
    }
}
