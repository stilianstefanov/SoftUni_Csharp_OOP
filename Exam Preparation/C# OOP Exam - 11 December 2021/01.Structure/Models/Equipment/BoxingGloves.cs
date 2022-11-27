namespace Gym.Models.Equipment
{
    public class BoxingGloves : Equipment
    {
        private const double InitialWeight = 227;
        private const decimal InitialPrice = 120;

        public BoxingGloves() : base(InitialWeight, InitialPrice)
        {
        }
    }
}
