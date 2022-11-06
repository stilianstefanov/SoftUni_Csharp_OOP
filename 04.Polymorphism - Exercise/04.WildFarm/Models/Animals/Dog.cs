

namespace WildFarm.Models.Animals
{
    using Abstracts;
    using System;

    public class Dog : Mammal
    {
        private const double weightPerFood = 0.40;
        public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        public override void Eat(Food food)
        {
            if (food.Type != "Meat")
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.Type}!");
            }

            this.Weight += food.Quantity * weightPerFood;
            this.FoodEaten += food.Quantity;
        }

        public override string ProduceSound()
            => "Woof!";


    }
}
