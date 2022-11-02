using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Citizen : IIdable
    {
        public Citizen(string name, int age, string id)
        {
            Name = name;
            Age = age;
            Id = id;            
        }

        public string Name { get; private set; }
        public string Id { get ; set ; }
        public int Age { get; private set; }

    }
}
