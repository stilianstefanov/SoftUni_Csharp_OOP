using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderControl
{
    public class BorderChecker
    {
        private List<IIdable> etities;

        public void EngineRun()
        {
            etities = new List<IIdable>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length == 3)
                {
                    etities.Add(new Citizen(tokens[0], int.Parse(tokens[1]), tokens[2], tokens[3]));
                }
                else
                {
                    etities.Add(new Robot(tokens[0], tokens[1]));
                }
            }

            string endsWith = Console.ReadLine();

            Console.WriteLine(string.Join(Environment.NewLine,
                etities.Select(i => i.Id).Where(id => id.EndsWith(endsWith))));
        }
    }
}
