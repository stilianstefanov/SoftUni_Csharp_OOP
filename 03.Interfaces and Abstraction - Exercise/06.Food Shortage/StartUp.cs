using BorderControl.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BorderControl
{
    public class StartUp
    {        
        static void Main(string[] args)
        {
            IEngine foodBuyer = new FoodBuyer();
            foodBuyer.Run();
        }       
    }
}
