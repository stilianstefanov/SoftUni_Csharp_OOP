using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.Enter_Numbers
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<int> numCollection = new List<int>();

            while (numCollection.Count < 10)
            {
                try
                {
                    if (!numCollection.Any())
                    {
                        numCollection.Add(ReadNumber(1, 100));
                    }
                    else
                    {
                        numCollection.Add(ReadNumber(numCollection.Max(), 100));
                    }
                }
                catch(ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine(String.Join(", ", numCollection));
        }

        private static int ReadNumber(int start, int end)
        {
            string input = Console.ReadLine();

            int num;
            
            bool isParsed = int.TryParse(input, out num);
            if (!isParsed)
            {
                throw new ArgumentException("Invalid Number!");
            }

            if (num <= start || num >= end)
            {
                throw new ArgumentException($"Your number is not in range {start} - {end}!");
            }

            return num;
        }
    }
}
