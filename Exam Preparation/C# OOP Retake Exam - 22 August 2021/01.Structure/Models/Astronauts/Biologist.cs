namespace SpaceStation.Models.Astronauts
{
    public class Biologist : Astronaut
    {
        private const double InitialOxygen = 70;

        public Biologist(string name) : base(name, InitialOxygen)
        {
        }

        public override void Breath()
        {
            double oxygenLeft = Oxygen - 5;
            if (oxygenLeft < 0)
                Oxygen = 0;
            else
                Oxygen = oxygenLeft;
        }
    }
}
