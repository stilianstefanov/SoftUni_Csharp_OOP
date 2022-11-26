namespace NavalVessels.Models
{
    using System;

    using Contracts;
    

    public class Battleship : Vessel, IBattleship
    {
        private const int InitialArmorThickness = 300;
        private bool sonarMode;
    

        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, InitialArmorThickness)
        {
            SonarMode = false;
        }

        public bool SonarMode { get => sonarMode; private set => sonarMode = value; }

        public void ToggleSonarMode()
        {
            SonarMode = !SonarMode;

            if (SonarMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 5;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 5;
            }
        }

        public override void RepairVessel()
        {
            if (ArmorThickness < InitialArmorThickness)
                ArmorThickness = InitialArmorThickness;
        }

        public override string ToString()
        {
            string sonarMode = SonarMode ? "ON" : "OFF";

            return base.ToString() + Environment.NewLine + $" *Sonar mode: {sonarMode}";
        }
    }
}
