using System;
using System.Collections.Generic;
using System.Linq;

namespace BorderControl
{
    public class StartUp
    {        
        static void Main(string[] args)
        {
            FoodBuyer foodBuyer = new FoodBuyer();
            foodBuyer.RunEngine();
        }       
    }
}
