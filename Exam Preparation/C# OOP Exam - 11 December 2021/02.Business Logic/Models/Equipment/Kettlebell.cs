namespace Gym.Models.Equipment
{
    public class Kettlebell : Equipment
    {
        private const double InitialWeight = 10000;
        private const decimal InitialPrice = 80;

        public Kettlebell() : base(InitialWeight, InitialPrice)
        {
        }
    }
}
