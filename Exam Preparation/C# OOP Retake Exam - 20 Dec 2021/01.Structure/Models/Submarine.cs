namespace NavalVessels.Models
{
    using System;

    using Contracts;
    

    public class Submarine : Vessel, ISubmarine
    {
        private const int InitialArmorThickness = 200;

        private bool submergeMode;

        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, InitialArmorThickness)
        {
            SubmergeMode = false;
        }

        public bool SubmergeMode { get => submergeMode; private set => submergeMode = value; }

        public void ToggleSubmergeMode()
        {
            SubmergeMode = !SubmergeMode;

            if (SubmergeMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 4;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 4;
            }
        }

        public override void RepairVessel()
        {
            if (ArmorThickness < InitialArmorThickness)
                ArmorThickness = InitialArmorThickness;
        }

        public override string ToString()
        {
            string submergeMode = SubmergeMode ? "ON" : "OFF";
            return base.ToString() + Environment.NewLine + $" *Submerge mode: {submergeMode}";
        }
    }
}
