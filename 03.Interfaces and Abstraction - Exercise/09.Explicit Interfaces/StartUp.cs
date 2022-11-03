using ExplicitInterfaces.Contracts;
using System;

namespace ExplicitInterfaces
{
    public class Start
    {
        static void Main(string[] args)
        {            
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var citizen = new Citizen(tokens[0], tokens[1], int.Parse(tokens[2]));

                IPerson person = citizen;
                IResident resident = citizen;

                person.GetName();
                resident.GetName();
            }
        }
    }
}
