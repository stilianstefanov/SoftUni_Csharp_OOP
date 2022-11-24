namespace Heroes.Models.Heroes
{
    using System;
    using System.Text;
    using Contracts;
    

    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;

        public Hero(string name, int health, int armour)
        {
            this.Name = name;
            this.Health = health;
            this.Armour = armour;
        }

        public string Name
        {
            get => name; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Hero name cannot be null or empty.");
                }
                name = value;
            }
        }

        public int Health
        {
            get => health;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hero health cannot be below 0.");
                }
                health = value;
            }
        }

        public int Armour
        {
            get => armour;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hero armour cannot be below 0.");
                }
                armour = value;
            }
        }

        public IWeapon Weapon
        {
            get => weapon;
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Weapon cannot be null.");
                }
                weapon = value;
            }
        }

        public bool IsAlive => Health > 0;

        public void AddWeapon(IWeapon weapon)
        {
            this.Weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            int armorLeft = this.Armour - points;
            
            if (armorLeft < 0)
            {
                this.Armour = 0;
                int healthLeft = this.Health - Math.Abs(armorLeft);

                if (healthLeft < 0)
                {
                    Health = 0;
                }
                else
                {
                    this.Health = healthLeft;
                }
            }
            else
            {
                this.Armour = armorLeft;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name}: {this.Name}");
            sb.AppendLine($"--Health: {this.Health}");
            sb.AppendLine($"--Armour: {this.Armour}");
            if (this.Weapon == null)
            {
                sb.AppendLine("--Weapon: Unarmed");
            }
            else
            {
                sb.AppendLine($"--Weapon: {this.Weapon.Name}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
