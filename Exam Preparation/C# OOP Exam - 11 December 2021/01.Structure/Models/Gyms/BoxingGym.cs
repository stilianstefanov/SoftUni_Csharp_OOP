namespace Gym.Models.Gyms
{
    public class BoxingGym : Gym
    {
        private const int InitialCapacity = 15;

        public BoxingGym(string name) : base(name, InitialCapacity)
        {
        }
    }
}
