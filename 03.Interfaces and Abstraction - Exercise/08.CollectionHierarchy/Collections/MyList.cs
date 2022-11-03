using CollectionHierarchy.Collections.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionHierarchy.Collections
{
    public class MyList : IList
    {
        public MyList()
        {
            Collection = new List<string>();
        }
        public int Used => Collection.Count;

        public List<string> Collection { get; private set; }

        public int Add(string item)
        {

            Collection.Insert(0, item);

            return 0;
        }

        public string Remove()
        {
            string itemToRemove = Collection.First();

            Collection.RemoveAt(0);

            return itemToRemove;
        }
    }
}
