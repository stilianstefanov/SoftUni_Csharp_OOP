using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionHierarchy.Collections.Contracts
{
    public interface IAddable
    {
        List<string> Collection { get; }

        int Add(string item);
    }
}
