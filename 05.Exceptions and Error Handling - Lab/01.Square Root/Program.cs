using System;

namespace _01.Square_Root
{
    public class Program
    {
        static void Main(string[] args)
        {
			try
			{
				int num = int.Parse(Console.ReadLine());
				if (num < 0)
					throw new ArgumentException("Invalid number.");

				Console.WriteLine(Math.Sqrt(num));
			}
			catch (ArgumentException ae)
			{

				Console.WriteLine(ae.Message);
			}
			finally
			{
				Console.WriteLine("Goodbye.");
			}
        }
    }
}
