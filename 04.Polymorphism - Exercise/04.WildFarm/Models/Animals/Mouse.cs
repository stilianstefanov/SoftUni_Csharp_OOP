

namespace WildFarm.Models.Animals
{
    using System;

    using Abstracts;

    public class Mouse : Mammal
    {
        private const double weightPerFood = 0.10;
        public Mouse(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        public override void Eat(Food food)
        {
            if (food.Type != "Vegetable" && food.Type != "Fruit")
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.Type}!");
            }

            this.Weight += food.Quantity * weightPerFood;
            this.FoodEaten += food.Quantity;
        }

        public override string ProduceSound()
            => "Squeak";


    }
}
