﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    public class Coffee : HotBeverage
    {
        public Coffee(string name, double caffeine) : base(name, 3.50M, 50)
        {
            this.Caffeine = caffeine;
            this.CoffeeMilliliters = 50;
            this.CoffeePrice = 3.50M;
        }

        public double CoffeeMilliliters { get; set; }
        public decimal CoffeePrice { get; set; }
        public double Caffeine { get; set; }
              
    }
}
