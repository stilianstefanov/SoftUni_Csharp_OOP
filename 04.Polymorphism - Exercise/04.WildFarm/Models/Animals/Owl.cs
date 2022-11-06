

namespace WildFarm.Models.Animals
{
    using Abstracts;
    using System;

    public class Owl : Bird
    {
        private const double weightPerFood = 0.25;
        public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
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
            => "Hoot Hoot";


    }
}
