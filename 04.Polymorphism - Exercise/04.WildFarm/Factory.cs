

using System;
using WildFarm.Models.Abstracts;
using WildFarm.Models.Animals;
using WildFarm.Models.Foods;

namespace WildFarm
{
    public static class Factory
    {
        public static Animal GetAnimal(string input)
        {
            string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Animal animal = null;

            string type = tokens[0];
            string name = tokens[1];
            double weight = double.Parse(tokens[2]);

            switch (type)
            {
                case "Cat":
                    animal = new Cat(name, weight, tokens[3], tokens[4]);
                    break;
                case "Tiger":
                    animal = new Tiger(name, weight, tokens[3], tokens[4]);
                    break;
                case "Dog":
                    animal = new Dog(name, weight, tokens[3]);
                    break;
                case "Mouse":
                    animal = new Mouse(name, weight, tokens[3]);
                    break;
                case "Owl":
                    animal = new Owl(name, weight, double.Parse(tokens[3]));
                    break;
                case "Hen":
                    animal = new Hen(name, weight, double.Parse(tokens[3]));
                    break;
                default:
                    break;
            }

            return animal;
        }

        public static Food GetFood(string[] foodTokens)
        {
            Food food = null;

            string type = foodTokens[0];
            int qty = int.Parse(foodTokens[1]);

            switch (type)
            {
                case "Fruit":
                    food = new Fruit(qty);
                    break;
                case "Meat":
                    food = new Meat(qty);
                    break;
                case "Seeds":
                    food = new Seeds(qty);
                    break;
                case "Vegetable":
                    food = new Vegetable(qty);
                    break;
                default:
                    break;
            }

            return food;
        }
    }
}
