﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Citizen : IIdable, IBirthdayable, IBuyer
    {
        public Citizen(string name, int age, string id, string birthday)
        {
            Name = name;
            Age = age;
            Id = id;
            Birthday = birthday;
            Food = 0;
        }

        public string Name { get; private set; }
        public string Id { get ; set ; }
        public int Age { get; private set; }

        public string Birthday { get; private set; }
        public int Food { get; private set; }

        public void BuyFood()
        {
            Food += 10;
        }
    }
}
