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
            var knights = players.Where(p => p.GetType().Name == "Knight").ToList();
            var barbarians = players.Where(p => p.GetType().Name == "Barbarian").ToList();

          
            while (true)
            {
                bool allKnightsDead = true;
                bool allBarbariansDead = true;

                int aliveKnightsCount = 0;
                int aliveBarbariansCount = 0;

                foreach (var knight in knights)
                {
                    if (knight.IsAlive)
                    {
                        allKnightsDead = false;
                        aliveKnightsCount++;

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
                        allBarbariansDead = false;
                        aliveBarbariansCount++;

                        foreach (var knight in knights)
                        {
                            int damage = barb.Weapon.DoDamage();
                            knight.TakeDamage(damage);
                        }
                    }
                }

                if (allBarbariansDead)
                {
                    return $"The knights took {knights.Count - aliveKnightsCount} casualties but won the battle.";
                }
                if (allKnightsDead)
                {
                    return $"The barbarians took {barbarians.Count - aliveBarbariansCount} casualties but won the battle.";
                }
            }
        }
    }
}
