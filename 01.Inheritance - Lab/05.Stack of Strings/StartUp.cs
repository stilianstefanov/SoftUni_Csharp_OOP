using System;

namespace CustomStack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            StackOfStrings stackOfStrings = new StackOfStrings();

            string[] test =
            {
                "Goshko",
                "Dimitrichko",
                "Pesho"
            };

            Console.WriteLine(string.Join(" ", stackOfStrings.AddRange(test)));
        }
    }
}
