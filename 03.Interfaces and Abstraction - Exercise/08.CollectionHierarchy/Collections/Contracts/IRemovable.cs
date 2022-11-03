using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionHierarchy.Collections.Contracts
{
    public interface IRemovable : IAddable
    {
        string Remove();
    }
}
