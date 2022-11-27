namespace Gym.Models.Gyms
{
    public class WeightliftingGym : Gym
    {
        private const int InitialCapacity = 20;

        public WeightliftingGym(string name) : base(name, InitialCapacity)
        {
        }
    }
}
