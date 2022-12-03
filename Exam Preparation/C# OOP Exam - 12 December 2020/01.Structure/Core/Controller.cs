namespace Bakery.Core
{
    using System.Linq;
    using System;
    using System.Text;
    using System.Collections.Generic;

    using Models.BakedFoods.Contracts;
    using Contracts;
    using Models.Drinks.Contracts;
    using Models.Tables.Contracts;
    using Models.Drinks;
    using Utilities.Messages;
    using Models.Tables;
    using Models.BakedFoods;

    public class Controller : IController
    {
        private List<IBakedFood> bakedFoods;
        private List<IDrink> drinks;
        private List<ITable> tables;
        private decimal totalIncome;

        public Controller()
        {
            bakedFoods= new List<IBakedFood>();
            drinks= new List<IDrink>();
            tables= new List<ITable>();
        }

        public string AddDrink(string type, string name, int portion, string brand)
        {
            IDrink newDrink = null;
            switch (type)
            {
                case "Tea":
                    newDrink = new Tea(name, portion, brand);
                    break;
                case "Water":
                    newDrink = new Water(name, portion, brand);
                    break;
            }

            drinks.Add(newDrink);

            return string.Format(OutputMessages.DrinkAdded, name, brand);
        }

        public string AddFood(string type, string name, decimal price)
        {
            IBakedFood newFood  = null;
            switch (type)
            {
                case "Bread":
                    newFood = new Bread(name, price);
                    break;
                case "Cake":
                    newFood = new Cake(name, price);
                    break;
            }

            bakedFoods.Add(newFood);

            return string.Format(OutputMessages.FoodAdded, name, type);
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable newTable = null;
            switch (type)
            {
                case "InsideTable":
                    newTable = new InsideTable(tableNumber, capacity);
                    break;
                case "OutsideTable":
                    newTable = new OutsideTable(tableNumber, capacity);
                    break;
            }

            tables.Add(newTable);

            return string.Format(OutputMessages.TableAdded, tableNumber);
        }

        public string GetFreeTablesInfo()
        {
            var freeTables = tables.Where(t => !t.IsReserved);
            var sb = new StringBuilder();

            foreach (var table in freeTables)
            {
                sb.AppendLine(table.GetFreeTableInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string GetTotalIncome()
            => $"Total income: {totalIncome:F2}lv";

        public string LeaveTable(int tableNumber)
        {
            var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);

            decimal bill = table.GetBill();
            totalIncome += bill;
            table.Clear();

            return $"Table: {tableNumber}" + Environment.NewLine + $"Bill: {bill:f2}";
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            var drink = drinks.FirstOrDefault(d => d.Name == drinkName && d.Brand == drinkBrand);

            if (table == null)
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            if (drink == null)
                return string.Format(OutputMessages.NonExistentDrink, drinkName, drinkBrand);

            table.OrderDrink(drink);

            return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            var table = tables.FirstOrDefault(t => t.TableNumber== tableNumber);
            var food = bakedFoods.FirstOrDefault(f => f.Name == foodName);

            if (table == null)
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            if (food == null)
                return string.Format(OutputMessages.NonExistentFood, foodName);

            table.OrderFood(food);

            return string.Format(OutputMessages.FoodOrderSuccessful, tableNumber, foodName);
        }

        public string ReserveTable(int numberOfPeople)
        {
            var tableToReserve = tables.FirstOrDefault(t => !t.IsReserved && t.Capacity >= numberOfPeople);
            if (tableToReserve == null)
                return string.Format(OutputMessages.ReservationNotPossible, numberOfPeople);

            tableToReserve.Reserve(numberOfPeople);

            return string.Format(OutputMessages.TableReserved, tableToReserve.TableNumber, numberOfPeople);
        }
    }
}
