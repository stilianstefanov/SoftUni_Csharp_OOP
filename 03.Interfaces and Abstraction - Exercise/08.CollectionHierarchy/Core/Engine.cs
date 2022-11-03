using CollectionHierarchy.Collections;
using CollectionHierarchy.Collections.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionHierarchy.Core
{
    public class Engine : IEngine
    {
        private IAddable addableCollection;
        private IRemovable removableCollection;
        private IList myList;
        public void Run()
        {
            addableCollection = new AddCollection();
            removableCollection = new AddRemoveCollection();
            myList = new MyList();

            StringBuilder addCollectonAddResult = new StringBuilder();
            StringBuilder removeCollectonAddResult = new StringBuilder();
            StringBuilder myListCollectonAddResult = new StringBuilder();
            StringBuilder removeCollectonRemoveResult = new StringBuilder();
            StringBuilder myListCollectonRemoveResult = new StringBuilder();

            string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < input.Length; i++)
            {
                addCollectonAddResult.Append($"{addableCollection.Add(input[i])} ");
                removeCollectonAddResult.Append($"{removableCollection.Add(input[i])} ");
                myListCollectonAddResult.Append($"{myList.Add(input[i])} ");
            }

            int removeCnt = int.Parse(Console.ReadLine());
            for (int i = 0; i < removeCnt; i++)
            {
                removeCollectonRemoveResult.Append($"{removableCollection.Remove()} ");
                myListCollectonRemoveResult.Append($"{myList.Remove()} ");
            }

            Console.WriteLine(addCollectonAddResult.ToString().TrimEnd());
            Console.WriteLine(removeCollectonAddResult.ToString().TrimEnd());
            Console.WriteLine(myListCollectonAddResult.ToString().TrimEnd());
            Console.WriteLine(removeCollectonRemoveResult.ToString().TrimEnd());
            Console.WriteLine(myListCollectonRemoveResult.ToString().TrimEnd());

        }
    }
}
