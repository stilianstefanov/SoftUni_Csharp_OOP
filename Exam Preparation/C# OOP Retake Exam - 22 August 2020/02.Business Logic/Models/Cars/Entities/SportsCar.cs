namespace EasterRaces.Models.Cars.Entities
{
    public class SportsCar : Car
    {
        private const double InitialCubicCentimeters = 3000;
        private const int InitialMinHP = 250;
        private const int InitialMaxHP = 450;

        public SportsCar(string model, int horsePower)
            : base(model, horsePower, InitialCubicCentimeters, InitialMinHP, InitialMaxHP)
        {
        }
    }
}
