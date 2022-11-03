using System;
using System.Collections.Generic;
using System.Text;

namespace ExplicitInterfaces.Contracts
{
    public interface IPerson
    {
        string Name { get; }
        int Age { get; }
        void GetName();
    }
}
