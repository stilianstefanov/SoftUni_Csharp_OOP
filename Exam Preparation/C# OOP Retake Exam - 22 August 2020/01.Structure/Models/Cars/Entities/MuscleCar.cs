namespace EasterRaces.Models.Cars.Entities
{
    public class MuscleCar : Car
    {
        private const double InitialCubicCentimeters = 5000;
        private const int InitialMinHP = 400;
        private const int InitialMaxHP = 600;

        public MuscleCar(string model, int horsePower)
            : base(model, horsePower, InitialCubicCentimeters, InitialMinHP, InitialMaxHP)
        {
        }
    }
}
