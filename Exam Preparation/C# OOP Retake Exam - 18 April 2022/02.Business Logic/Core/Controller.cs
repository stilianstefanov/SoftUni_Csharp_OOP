namespace Heroes.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.Heroes;
    using Models.Map;
    using Models.Weapons;    
    using Models.Contracts;
    using Repositories;
    using Repositories.Contracts;
  

    partial class Controller : IController
    {
        private IRepository<IHero> heroes;
        private IRepository<IWeapon> weapons;

        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            var hero = heroes.FindByName(heroName);
            if (hero == null)
            {
                throw new InvalidOperationException($"Hero {heroName} does not exist.");
            }

            var weapon = weapons.FindByName(weaponName);
            if (weapon == null)
            {
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            }

            if (hero.Weapon != null)
            {
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            }

            hero.AddWeapon(weapon);
            weapons.Remove(weapon);

            return $"Hero {heroName} can participate in battle using a {weapon.GetType().Name.ToLower()}.";
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            if (heroes.Models.Any(h => h.Name == name))
            {
                throw new InvalidOperationException($"The hero {name} already exists.");
            }
            if (type != "Knight" && type != "Barbarian")
            {
                throw new InvalidOperationException("Invalid hero type.");
            }

            IHero newHero = null;
            string output = string.Empty;

            switch (type)
            {
                case "Knight":
                    {
                        newHero = new Knight(name, health, armour);

                        output = $"Successfully added Sir {name} to the collection.";
                    }
                    break;
                case "Barbarian":
                    {
                        newHero = new Barbarian(name, health, armour);

                        output = $"Successfully added Barbarian {name} to the collection.";
                    }
                    break;
            }

            heroes.Add(newHero);
            return output;
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            if (weapons.Models.Any(w => w.Name == name))
            {
                throw new InvalidOperationException($"The weapon {name} already exists.");
            }
            if (type != "Claymore" && type != "Mace")
            {
                throw new InvalidOperationException("Invalid weapon type.");
            }

            IWeapon newWeapon = null;

            switch (type)
            {
                case "Claymore":
                    newWeapon = new Claymore(name, durability);
                    break;
                case "Mace":
                    newWeapon = new Mace(name, durability);
                    break;              
            }

            weapons.Add(newWeapon);
            return $"A {type.ToLower()} {name} is added to the collection.";
        }

        public string HeroReport()
        {
            var sb = new StringBuilder();

            foreach (var hero in heroes.Models.OrderBy(h => h.GetType().Name).ThenByDescending(h => h.Health).ThenBy(h => h.Name))
            {
                sb.AppendLine(hero.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string StartBattle()
        {
            var map = new Map();

            var heroesToBattle = heroes.Models.Where(h => h.IsAlive && h.Weapon != null).ToList();

            return map.Fight(heroesToBattle);
        }
    }
}
