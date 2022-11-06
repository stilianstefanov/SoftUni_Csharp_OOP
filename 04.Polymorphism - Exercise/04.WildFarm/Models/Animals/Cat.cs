

namespace WildFarm.Models.Animals
{
    using Abstracts;
    using System;

    public class Cat : Feline
    {
        private const double weightPerFood = 0.30;
        public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }

        public override void Eat(Food food)
        {
            if (food.Type != "Vegetable" && food.Type != "Meat")
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.Type}!");
            }

            this.Weight += food.Quantity * weightPerFood;
            this.FoodEaten += food.Quantity;
        }

        public override string ProduceSound()
            => "Meow";
    }
}
