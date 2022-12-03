namespace Bakery.Models.Tables
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Text;

    using BakedFoods.Contracts;
    using Drinks.Contracts;
    using Contracts;
    using Utilities.Messages;   

    public abstract class Table : ITable
    {
        private ICollection<IBakedFood> foodOrders;
        private ICollection<IDrink> drinkOrders;
        private int tableNumber;
        private int capacity;
        private int numberOfPeople;
        private decimal pricePerPerson;
        private bool isReserved;

        protected Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            TableNumber= tableNumber;
            Capacity= capacity;
            PricePerPerson= pricePerPerson;
            foodOrders= new List<IBakedFood>();
            drinkOrders= new List<IDrink>();
        }

        public int TableNumber
        {
            get => tableNumber;
            private set => tableNumber = value;
        }

        public int Capacity
        {
            get => capacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }
                capacity = value;
            }
        }

        public int NumberOfPeople
        {
            get => numberOfPeople;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }
                numberOfPeople = value;
            }
        }

        public decimal PricePerPerson
        {
            get => pricePerPerson;
            private set => pricePerPerson = value;
        }

        public bool IsReserved
        {
            get => isReserved;
            private set => isReserved = value;
        }

        public decimal Price => PricePerPerson * NumberOfPeople;

        public void Clear()
        {
            foodOrders.Clear();
            drinkOrders.Clear();
            IsReserved = false;
            numberOfPeople = 0;
        }

        public decimal GetBill()
        {
            return Price + foodOrders.Sum(o => o.Price) + drinkOrders.Sum(o => o.Price);
        }

        public string GetFreeTableInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Table: {TableNumber}");
            sb.AppendLine($"Type: {this.GetType().Name}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Price per Person: {PricePerPerson}");

            return sb.ToString().TrimEnd();
        }

        public void OrderDrink(IDrink drink)
        {
            drinkOrders.Add(drink);
        }

        public void OrderFood(IBakedFood food)
        {
            foodOrders.Add(food);
        }

        public void Reserve(int numberOfPeople)
        {
            IsReserved = true;
            NumberOfPeople = numberOfPeople;
        }
    }
}
