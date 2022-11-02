using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Citizen : IIdable, IBirthdayable
    {
        public Citizen(string name, int age, string id, string birthday)
        {
            Name = name;
            Age = age;
            Id = id;
            Birthday = birthday;
        }

        public string Name { get; private set; }
        public string Id { get ; set ; }
        public int Age { get; private set; }

        public string Birthday { get; private set; }
    }
}
