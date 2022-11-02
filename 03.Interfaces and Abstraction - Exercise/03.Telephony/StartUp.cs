using System;

namespace Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] numbers = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] urls = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            ICall caller;           
            for (int i = 0; i < numbers.Length; i++)
            {
                string number = numbers[i];
                try
                {
                    if (number.Length == 7)
                    {
                        caller = new StationaryPhone();
                        caller.Call(number);
                    }
                    else
                    {
                        caller = new Smartphone();
                        caller.Call(number);
                    }
                }
                catch(Exception e) { Console.WriteLine(e.Message); }
            }
            IBrowse browser = new Smartphone();
            for (int i = 0; i < urls.Length; i++)
            {
                try
                {
                    string url = urls[i];
                    browser.Browse(url);
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }
    }
}
