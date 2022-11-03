using CollectionHierarchy.Collections.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionHierarchy.Collections
{
    public class AddRemoveCollection : IRemovable
    {
        public AddRemoveCollection()
        {
            Collection = new List<string>();
        }
        public List<string> Collection { get; private set; }

        public int Add(string item)
        {
            Collection.Insert(0, item);

            return 0;
        }

        public string Remove()
        {
            string itemToRemove = Collection[Collection.Count - 1];

            Collection.RemoveAt(Collection.Count - 1);

            return itemToRemove;
        }
    }
}
