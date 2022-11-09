using System;

namespace _04.Sum_of_Integers
{
    public class Program
    {
        private static int sum;

        static void Main(string[] args)
        {
            string[] tokens = Console.ReadLine().Split();

            for (int i = 0; i < tokens.Length; i++)
            {
                string value = tokens[i];

                AddNumber(value);
            }

            Console.WriteLine($"The total sum of all integers is: {sum}");
        }

        private static void AddNumber(string value)
        {
            try
            {
                int valueToAdd = int.Parse(value);
                sum += valueToAdd;
            }
            catch (FormatException)
            {
                Console.WriteLine($"The element '{value}' is in wrong format!");
            }
            catch (OverflowException)
            {
                Console.WriteLine($"The element '{value}' is out of range!");
            }
            finally
            {
                Console.WriteLine($"Element '{value}' processed - current sum: {sum}");
            }
        }
    }
}
