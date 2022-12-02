namespace WarCroft.Entities.Inventory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Items;
    using Constants;

    public abstract class Bag : IBag
    {        
        private List<Item> items;

        protected Bag(int capacity)
        {
            Capacity = capacity;
            items= new List<Item>();
        }

        public int Capacity { get; set; } = 100;

        public int Load => items.Select(x => x.Weight).Sum();

        public IReadOnlyCollection<Item> Items => items.AsReadOnly();

        public void AddItem(Item item)
        {
            if (Load + item.Weight > Capacity)
                throw new InvalidOperationException(ExceptionMessages.ExceedMaximumBagCapacity);

            items.Add(item);
        }

        public Item GetItem(string name)
        {
            if (!items.Any())
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);

            var itemToReturn = items.FirstOrDefault(i => i.GetType().Name == name);
            if (itemToReturn == null)
                throw new ArgumentException(string.Format(ExceptionMessages.ItemNotFoundInBag, name));

            items.Remove(itemToReturn);
            return itemToReturn;
        }
    }
}
