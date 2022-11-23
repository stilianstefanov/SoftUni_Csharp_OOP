namespace PlanetWars.Models.Planets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Contracts;
    using MilitaryUnits.Contracts;
    using Utilities.Messages;
    using Repositories.Contracts;
    using Weapons.Contracts;
    using Repositories;

    public class Planet : IPlanet
    {
        private IRepository<IMilitaryUnit> units;
        private IRepository<IWeapon> weapons;
        private string name;
        private double budget;

        public Planet(string name, double budget)
        {
            this.Name = name;
            this.Budget = budget;
            units = new UnitRepository();
            weapons = new WeaponRepository();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }
                name = value;
            }
        }

        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }
            }
        }

        public double MilitaryPower => GetTotalPower();

        public IReadOnlyCollection<IMilitaryUnit> Army => units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            units.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");
            if (!Army.Any())
            {
                sb.AppendLine("--Forces: No units");
            }
            else
            {
                sb.AppendLine($"--Forces: {string.Join(", ", Army.Select(u => u.GetType().Name))}");
            }
            if (!Weapons.Any())
            {
                sb.AppendLine("--Combat equipment: No weapons");
            }
            else
            {
                sb.AppendLine($"Combat equipment: {string.Join(", ", Weapons.Select(u => u.GetType().Name))}");
            }
            sb.Append($"--Military Power: {MilitaryPower}");

            return sb.ToString().TrimEnd();
        }

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public void Spend(double amount)
        {
            if (amount > Budget)
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }
            Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var unit in Army)
            {
                unit.IncreaseEndurance();
            }
        }

        private double GetTotalPower()
        {
            double totalPower = units.Models.Sum(u => u.EnduranceLevel) + weapons.Models.Sum(w => w.DestructionLevel);

            if (units.Models.Any(u => u.GetType().Name == "AnonymousImpactUnit"))
            {
                totalPower += totalPower * 0.30;
            }
            if (weapons.Models.Any(w => w.GetType().Name == "NuclearWeapon"))
            {
                totalPower += totalPower * 0.45;
            }

            return Math.Round(totalPower, 3);
        }
    }
}
