namespace PlanetWars.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.Planets.Contracts;
    using Models.Planets;
    using Utilities.Messages;
    using Repositories.Contracts;
    using Models.MilitaryUnits.Contracts;
    using Models.MilitaryUnits;
    using Models.Weapons.Contracts;
    using Models.Weapons;
    using Repositories;

    public class Controller : IController
    {
        private IRepository<IPlanet> planets;

        public Controller()
        {
            this.planets = new PlanetRepository();
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            var planet = planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (unitTypeName != "AnonymousImpactUnit" && unitTypeName != "SpaceForces" && unitTypeName != "StormTroopers")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }
            if (planet.Army.Any(u => u.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }

            IMilitaryUnit newUnit = CreatNewUnit(unitTypeName);
            planet.Spend(newUnit.Cost);
            planet.AddUnit(newUnit);

            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            var planet = planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (weaponTypeName != "BioChemicalWeapon" && weaponTypeName != "NuclearWeapon" && weaponTypeName != "SpaceMissiles")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }
            if (planet.Weapons.Any(u => u.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }

            IWeapon newWeapon = CreateNewWeapon(weaponTypeName, destructionLevel);
            planet.Spend(newWeapon.Price);
            planet.AddWeapon(newWeapon);

            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            if (planets.Models.Any(p => p.Name == name))
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }
            planets.AddItem(new Planet(name, budget));

            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string ForcesReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var planet in planets.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p => p.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            var attackerPlanet = planets.FindByName(planetOne);
            var deffenderPlanet = planets.FindByName(planetTwo);

            bool attackerHasNuclear = attackerPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon");
            bool deffenderHasNuclear = deffenderPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon");

            if (attackerPlanet.MilitaryPower == deffenderPlanet.MilitaryPower)
            {
                if (attackerHasNuclear && deffenderHasNuclear)
                {
                    return NoWinnerAction(attackerPlanet, deffenderPlanet);
                }
                else if (!attackerHasNuclear && !deffenderHasNuclear)
                {                    
                    return NoWinnerAction(attackerPlanet, deffenderPlanet);
                }
                else if (attackerHasNuclear)
                {
                    return WinnerAction(attackerPlanet, deffenderPlanet);
                }
                else if (deffenderHasNuclear)
                {
                    return WinnerAction(deffenderPlanet, attackerPlanet);
                }
            }
            else
            {
                if (attackerPlanet.MilitaryPower > deffenderPlanet.MilitaryPower)
                {
                    return WinnerAction(attackerPlanet, deffenderPlanet);
                }
                else
                {
                    return WinnerAction(deffenderPlanet, attackerPlanet);
                }
            }
            return null;
        }
        
        public string SpecializeForces(string planetName)
        {
            var planet = planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (!planet.Army.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.NoUnitsFound);
            }
            planet.Spend(1.25);
            planet.TrainArmy();

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }

        private static string NoWinnerAction(IPlanet attackerPlanet, IPlanet deffenderPlanet)
        {
            attackerPlanet.Spend(attackerPlanet.Budget / 2);
            deffenderPlanet.Spend(deffenderPlanet.Budget / 2);

            return OutputMessages.NoWinner;
        }

        private string WinnerAction(IPlanet winnerPlanet, IPlanet defeatedPlanet)
        {
            winnerPlanet.Spend(winnerPlanet.Budget / 2);
            winnerPlanet.Profit(defeatedPlanet.Budget / 2);

            double unitsAndWeaponsTotalCost = GetWeaponsAndArmyTotalCost(defeatedPlanet);
            winnerPlanet.Profit(unitsAndWeaponsTotalCost);

            planets.RemoveItem(defeatedPlanet.Name);

            return string.Format(OutputMessages.WinnigTheWar, winnerPlanet.Name, defeatedPlanet.Name);
        }

        private IMilitaryUnit CreatNewUnit(string unitTypeName)
        {
            IMilitaryUnit newUnit = null;
            switch (unitTypeName)
            {
                case "AnonymousImpactUnit":
                    newUnit = new AnonymousImpactUnit();
                    break;
                case "SpaceForces":
                    newUnit = new SpaceForces();
                    break;
                case "StormTroopers":
                    newUnit = new StormTroopers();
                    break;
            }

            return newUnit;
        }

        private IWeapon CreateNewWeapon(string weaponTypeName, int destructionLevel)
        {
            IWeapon newWeapon = null;
            switch (weaponTypeName)
            {
                case "BioChemicalWeapon":
                    newWeapon = new BioChemicalWeapon(destructionLevel);
                    break;
                case "NuclearWeapon":
                    newWeapon = new NuclearWeapon(destructionLevel);
                    break;
                case "SpaceMissiles":
                    newWeapon = new SpaceMissiles(destructionLevel);
                    break;
            }

            return newWeapon;
        }

        private double GetWeaponsAndArmyTotalCost(IPlanet planet)
        {
            double totalCost = 0;

            if (planet.Army.Any())
            {
                totalCost += planet.Army.Sum(u => u.Cost);
            }
            if (planet.Weapons.Any())
            {
                totalCost += planet.Weapons.Sum(w => w.Price);
            }

            return totalCost;
        }
    }
}
