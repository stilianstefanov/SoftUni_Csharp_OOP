using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderControl
{
    public class FoodBuyer
    {
        private List<IBuyer> buyers;

        public void RunEngine()
        {
            buyers = new List<IBuyer>();

            int inputCnt = int.Parse(Console.ReadLine());
            for (int i = 0; i < inputCnt; i++)
            {
                string[] tokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                int age = int.Parse(tokens[1]);

                if (tokens.Length == 3)
                {
                    string group = tokens[2];
                    buyers.Add(new Rebel(name, age, group));
                }
                else
                {
                    string id = tokens[2];
                    string birthday = tokens[3];
                    buyers.Add(new Citizen(name, age, id, birthday));
                }
            }

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string name = input;

                IBuyer buyer = buyers.FirstOrDefault(b => b.Name == name);
                if (buyer == null) continue;

                buyer.BuyFood();
            }

            Console.WriteLine(buyers.Sum(b => b.Food));
        }
    }
}
