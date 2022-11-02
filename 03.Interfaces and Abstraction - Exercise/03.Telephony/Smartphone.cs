using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Telephony
{
    public class Smartphone : IBrowse, ICall
    {
        public void Browse(string url)
        {
            if (url.Any(ch => char.IsNumber(ch)))
            {
                throw new ArgumentException("Invalid URL!");
            }
            Console.WriteLine($"Browsing: {url}!");
        }

        public void Call(string number)
        {
            if (number.Any(ch => char.IsLetter(ch)))
            {
                throw new ArgumentException("Invalid number!");
            }
            Console.WriteLine($"Calling... {number}");
        }
    }
}
