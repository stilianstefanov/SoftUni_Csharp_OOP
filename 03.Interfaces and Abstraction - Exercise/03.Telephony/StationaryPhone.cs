using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telephony
{
    public class StationaryPhone : ICall
    {
        public void Call(string number)
        {
            if (number.Any(ch => char.IsLetter(ch)))
            {
                throw new ArgumentException("Invalid number!");
            }
            Console.WriteLine($"Dialing... {number}");
        }
    }
}
