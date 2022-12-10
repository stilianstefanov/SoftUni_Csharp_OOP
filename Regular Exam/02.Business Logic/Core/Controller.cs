namespace ChristmasPastryShop.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;

    using Models.Booths;
    using Models.Booths.Contracts;
    using Models.Cocktails;
    using Models.Cocktails.Contracts;
    using Models.Delicacies;
    using Models.Delicacies.Contracts;

    using Repositories;
    using Repositories.Contracts;
    using Utilities.Messages;

    public class Controller : IController
    {
        private IRepository<IBooth> booths;

        public Controller()
        {
            booths = new BoothRepository();
        }


        public string AddBooth(int capacity)
        {
            int boothId = booths.Models.Count + 1;

            var newBooth = new Booth(boothId, capacity);

            booths.AddModel(newBooth);

            return string.Format(OutputMessages.NewBoothAdded, boothId, capacity);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {            
            if (cocktailTypeName != "Hibernation" && cocktailTypeName != "MulledWine")
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);        

            var boothToAddCocktail = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            if (size != "Small" && size != "Middle" && size != "Large")
                return string.Format(OutputMessages.InvalidCocktailSize, size);

            if (boothToAddCocktail.CocktailMenu.Models.Any(c => c.Name == cocktailName && c.Size == size))
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);

            ICocktail newCocktail = null;
            switch (cocktailTypeName)
            {
                case "Hibernation":
                    newCocktail = new Hibernation(cocktailName, size);
                    break;
                case "MulledWine":
                    newCocktail = new MulledWine(cocktailName, size);
                    break;
            }

            boothToAddCocktail.CocktailMenu.AddModel(newCocktail);

            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            
            if (delicacyTypeName != "Gingerbread" && delicacyTypeName != "Stolen")
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);

            var boothToAddDelicacy = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            if (boothToAddDelicacy.DelicacyMenu.Models.Any(d => d.Name == delicacyName))
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);

            IDelicacy newDelicacy = null;
            switch (delicacyTypeName)
            {
                case "Gingerbread":
                    newDelicacy = new Gingerbread(delicacyName);
                    break;
                case "Stolen":
                    newDelicacy = new Stolen(delicacyName);
                    break;
            }

            boothToAddDelicacy.DelicacyMenu.AddModel(newDelicacy);

            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string BoothReport(int boothId)
        {
            var boothToReport = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            return boothToReport.ToString();
        }

        public string LeaveBooth(int boothId)
        {
            var boothToLeave = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            double currentBill = boothToLeave.CurrentBill;

            boothToLeave.Charge();
            boothToLeave.ChangeStatus();

            var sb = new StringBuilder();
            sb.AppendLine($"Bill {currentBill:f2} lv");
            sb.AppendLine($"Booth {boothId} is now available!");

            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)
        {
            var boothsAvaliable = booths.Models.Where(b => !b.IsReserved && b.Capacity >= countOfPeople)
                .OrderBy(b => b.Capacity)
                .ThenByDescending(b => b.BoothId)
                .ToList();

            var boothToReserve = boothsAvaliable.FirstOrDefault();

            if (boothToReserve == null)
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);

            boothToReserve.ChangeStatus();

            return string.Format(OutputMessages.BoothReservedSuccessfully, boothToReserve.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            var booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            string[] splittedOrder = order.Split("/", StringSplitOptions.RemoveEmptyEntries);

            string itemTypeName = splittedOrder[0];
            string itemName = splittedOrder[1];
            int piecesCount = int.Parse(splittedOrder[2]);

            string cockTailSize = string.Empty;

            if (splittedOrder.Length == 4)
                cockTailSize = splittedOrder[3];           

            if (itemTypeName != "Gingerbread" && itemTypeName != "Stolen" 
                && itemTypeName != "Hibernation" && itemTypeName != "MulledWine")
                   return string.Format(OutputMessages.NotRecognizedType, itemTypeName);

            if (cockTailSize == string.Empty)
            {
                var delicacy = booth.DelicacyMenu.Models
                    .FirstOrDefault(d => d.Name == itemName);

                if (delicacy == null)
                    return string.Format(OutputMessages.CocktailStillNotAdded, itemTypeName, itemName);
            }
            else
            {
                var cocktail = booth.CocktailMenu.Models
                    .FirstOrDefault(d => d.Name == itemName);

                if (cocktail == null)
                    return string.Format(OutputMessages.CocktailStillNotAdded, itemTypeName, itemName);
            }

            if (cockTailSize == string.Empty)
            {
                var delicacy = booth.DelicacyMenu.Models
                    .FirstOrDefault(d => d.Name == itemName && d.GetType().Name == itemTypeName);

                if (delicacy == null)
                    return string.Format(OutputMessages.CocktailStillNotAdded, itemTypeName, itemName);

                double priceAmmount = delicacy.Price * piecesCount;
                booth.UpdateCurrentBill(priceAmmount);

                return string.Format(OutputMessages.SuccessfullyOrdered, booth.BoothId, piecesCount, itemName);
            }
            else
            {
                var cocktail = booth.CocktailMenu.Models
                    .FirstOrDefault(d => d.Name == itemName 
                    && d.GetType().Name == itemTypeName 
                    && d.Size == cockTailSize);

                if (cocktail == null)
                    return string.Format(OutputMessages.CocktailStillNotAdded, cockTailSize, itemName);

                double priceAmmount = cocktail.Price * piecesCount;
                booth.UpdateCurrentBill(priceAmmount);

                return string.Format(OutputMessages.SuccessfullyOrdered, booth.BoothId, piecesCount, itemName);
            }
        }
    }
}
