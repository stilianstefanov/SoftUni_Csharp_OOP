using CollectionHierarchy.Collections.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionHierarchy.Collections
{
    public class AddCollection : IAddable
    {
        public AddCollection()
        {
            Collection = new List<string>();
        }
        public List<string> Collection { get; private set; }

        public int Add(string item)
        {
            int indexToAddAt = Collection.Count;

            Collection.Add(item);

            return indexToAddAt;
        }
    }
}
