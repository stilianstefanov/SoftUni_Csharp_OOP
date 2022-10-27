using System;

namespace Restaurant
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Coffee coffee = new Coffee("capucino", 10);
            Console.WriteLine(coffee.CoffeePrice);
            
        }
    }
}