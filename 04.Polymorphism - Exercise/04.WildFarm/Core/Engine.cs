
namespace WildFarm.Core
{
    using System;
    using System.Collections.Generic;

    using Contracts;
    using IO.Contracts;
    using WildFarm.Models.Abstracts;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private List<Animal> animals;
        

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            animals = new List<Animal>();
        }

        public void Run()
        {
           
            string input;
            while ((input = reader.ReadLine()) != "End")
            {
                string[] foodTokens = reader.ReadLine().Split(" ", System.StringSplitOptions.RemoveEmptyEntries);

                Animal animal = Factory.GetAnimal(input);
                Food food = Factory.GetFood(foodTokens);

                writer.WriteLine(animal.ProduceSound());

                try
                {
                    animal.Eat(food);
                }
                catch(Exception e)
                {
                    writer.WriteLine(e.Message);
                }

                animals.Add(animal);
            }

            foreach (Animal animal in animals)
            {
                writer.WriteLine(animal.ToString());
            }
        }
    }
}
