﻿namespace Heroes.Models.Weapons
{
    public class Mace : Weapon
    {
        private const int DAMAGE = 25;

        public Mace(string name, int durability) : base(name, durability)
        {
        }

        public override int DoDamage()
        {
            if (Durability == 0)
            {
                return 0;
            }

            Durability--;
            return DAMAGE;
        }
    }
}
