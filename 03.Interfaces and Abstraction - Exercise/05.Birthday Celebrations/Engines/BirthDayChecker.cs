using BorderControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderControl
{   
    public class BirthDayChecker
    {
        private List<IBirthdayable> entities;

        public void EngineRun()
        {
            entities = new List<IBirthdayable>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string entityType = tokens[0];
                switch (entityType)
                {
                    case "Citizen":
                        entities.Add(new Citizen(tokens[1], int.Parse(tokens[2]), tokens[3], tokens[4]));
                        break;
                    case "Pet":
                        entities.Add(new Pet(tokens[1], tokens[2]));
                        break;
                }
            }

            string targetYear = Console.ReadLine();

            Console.WriteLine(string.Join(Environment.NewLine,
                entities.Select(e => e.Birthday).Where(b => b.EndsWith(targetYear))));
        }
    }
}
