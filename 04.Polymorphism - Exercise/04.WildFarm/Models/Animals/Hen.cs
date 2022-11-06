

namespace WildFarm.Models.Animals
{
    using Abstracts;
    public class Hen : Bird
    {
        private const double weightPerFood = 0.35;

        public Hen(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }

        public override void Eat(Food food)
        {
            this.Weight += food.Quantity * weightPerFood;
            this.FoodEaten += food.Quantity;
        }

        public override string ProduceSound()
            => "Cluck";


    }
}
