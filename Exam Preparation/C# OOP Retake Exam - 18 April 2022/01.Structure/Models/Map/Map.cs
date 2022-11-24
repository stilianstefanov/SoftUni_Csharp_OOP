namespace Heroes.Models.Map
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    

    public class Map : IMap
    {
        public Map()
        {

        }

        public string Fight(ICollection<IHero> players)
        {
            var knights = players.Where(p => p.GetType().Name == "Knight" && p.IsAlive);
            var barbarians = players.Where(p => p.GetType().Name == "Barbarian" && p.IsAlive);

            while (true)
            {
                foreach (var knight in knights)
                {
                    if (knight.IsAlive)
                    {
                        foreach (var barb in barbarians)
                        {
                            int damage = knight.Weapon.DoDamage();
                            barb.TakeDamage(damage);
                        }
                    }
                }

                foreach (var barb in barbarians)
                {
                    if (barb.IsAlive)
                    {
                        foreach (var knight in knights)
                        {
                            int damage = barb.Weapon.DoDamage();
                            knight.TakeDamage(damage);
                        }
                    }
                }

                if (!barbarians.Any(b => b.IsAlive))
                {
                    return $"The knights took {knights.Count(k => !k.IsAlive)} casualties but won the battle.";
                }
                if (!knights.Any(k => k.IsAlive))
                {
                    return $"The barbarians took {barbarians.Count(k => !k.IsAlive)} casualties but won the battle.";
                }
            }
        }
    }
}
