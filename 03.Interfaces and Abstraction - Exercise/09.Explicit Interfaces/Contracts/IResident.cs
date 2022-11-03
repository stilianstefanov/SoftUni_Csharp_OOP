using System;
using System.Collections.Generic;
using System.Text;

namespace ExplicitInterfaces.Contracts
{
    public interface IResident
    {
        string Name { get; }
        string Country { get; }
        void GetName();
    }
}
