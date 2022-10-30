using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();

            string[] personsInfo = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < personsInfo.Length; i++)
            {
                try
                {
                    string[] currPerson = personsInfo[i].Split("=");
                    string name = currPerson[0];
                    double money = double.Parse(currPerson[1]);
                    persons.Add(new Person(name, money));
                }
                catch(ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            List<Product> products = new List<Product>();

            string[] productsInfo = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < productsInfo.Length; i++)
            {
                try
                {
                    string[] currProduct = productsInfo[i].Split("=");
                    string name = currProduct[0];
                    double cost = double.Parse(currProduct[1]);
                    products.Add(new Product(name, cost));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string personName = command.Split(" ", StringSplitOptions.RemoveEmptyEntries).First();
                string productName = command.Split(" ", StringSplitOptions.RemoveEmptyEntries).Last();

                var person = persons.Find(p => p.Name == personName);
                person.BuyProduct(products.Find(pr => pr.Name == productName));
            }

            Console.WriteLine(string.Join(Environment.NewLine, persons));
        }
    }
}
