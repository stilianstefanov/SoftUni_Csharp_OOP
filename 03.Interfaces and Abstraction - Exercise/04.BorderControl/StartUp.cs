using System;
using System.Collections.Generic;
using System.Linq;

namespace BorderControl
{
    public class StartUp
    {
        private static List<IIdable> idables;

        static void Main(string[] args)
        {
            idables = new List<IIdable>();

            EngineRun();
        }

        private static void EngineRun()
        {
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length == 3)
                {
                    idables.Add(new Citizen(tokens[0], int.Parse(tokens[1]), tokens[2]));
                }
                else
                {
                    idables.Add(new Robot(tokens[0], tokens[1]));
                }
            }

            string endsWith = Console.ReadLine();

            Console.WriteLine(string.Join(Environment.NewLine,
                idables.Select(i => i.Id).Where(id => id.EndsWith(endsWith))));
        }
    }
}
