using System;
using System.Linq;

namespace _05.Play_Catch
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
          
            int exceptionsCnt = 0;
            while (exceptionsCnt < 3)
            {
                string[] cmdArgs = Console.ReadLine().Split();

                string command = cmdArgs[0];

                try
                {
                    switch (command)
                    {
                        case "Replace":
                            {
                                int atIndex = int.Parse(cmdArgs[1]);
                                int value = int.Parse(cmdArgs[2]);
                                numbers[atIndex] = value;
                            }
                            break;
                        case "Print":
                            {
                                int startIndex = int.Parse(cmdArgs[1]);
                                int endIndex = int.Parse(cmdArgs[2]);

                                Console.WriteLine(string.Join(", ", numbers.ToList().GetRange(startIndex, endIndex)));
                            }
                            break;
                        case "Show":
                            {
                                int atIndex = int.Parse(cmdArgs[1]);
                                Console.WriteLine(numbers[atIndex]);
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch(FormatException)
                {
                    exceptionsCnt++;
                    Console.WriteLine("The variable is not in the correct format!");
                }
                catch (IndexOutOfRangeException)
                {
                    exceptionsCnt++;
                    Console.WriteLine("The index does not exist!");
                }
                catch(ArgumentException)
                {
                    exceptionsCnt++;
                    Console.WriteLine("The index does not exist!");
                }
            }

            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}
