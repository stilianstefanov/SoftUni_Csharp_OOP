

namespace Raiding.Models
{
    internal class Paladin : BaseHero
    {
        private const int POWER = 100;
        public Paladin(string name) : base(name, POWER)
        {
        }

        public override string CastAbility()
            => $"{this.GetType().Name} - {Name} healed for {Power}";
    }
}
