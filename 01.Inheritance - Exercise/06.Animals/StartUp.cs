using System;
using System.Collections.Generic;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();

            while (true)
            {
                string animalType = Console.ReadLine();
                if (animalType == "Beast!") break;

                string[] tokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = tokens[0];
                int age = int.Parse(tokens[1]);
                if (age < 0)
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }
                if (animalType == "Kitten" || animalType == "Tomcat")
                {
                    if (animalType == "Kitten")
                    {                       
                        animals.Add(new Kitten(name, age));
                    }
                    else
                    {                       
                        animals.Add(new Tomcat(name, age));
                    }
                    continue;
                }

                string gender = tokens[2];
                if (gender != "Male" && gender != "Female")
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                if (animalType == "Dog")
                {
                    animals.Add(new Dog(name, age, gender));
                }
                else if (animalType == "Cat")
                {
                    animals.Add(new Cat(name, age, gender));
                }
                else if (animalType == "Frog")
                {
                    animals.Add(new Frog(name, age, gender));
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, animals));
        }
    }
}
