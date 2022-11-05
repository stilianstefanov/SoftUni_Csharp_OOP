
namespace Raiding.Models.Contracts
{
    public interface IHero
    {
        int Power { get; }
        string CastAbility();
    }
}
