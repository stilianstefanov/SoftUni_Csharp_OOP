namespace ChristmasPastryShop.Models.Booths
{
    using System;
    using System.Text;

    using Contracts;

    using Cocktails.Contracts;
    using Delicacies.Contracts;

    using Repositories;
    using Repositories.Contracts;

    using Utilities.Messages;

    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private IRepository<IDelicacy> delicacyMenu;
        private IRepository<ICocktail> cocktailMenu;
        private double currentBill;
        private double turnover;
        private bool isReserved;

        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;
            delicacyMenu = new DelicacyRepository();
            cocktailMenu= new CocktailRepository();

            CurrentBill = 0;
            Turnover = 0;
            IsReserved = false;
        }

        public int BoothId
        {
            get => boothId;
            private set => boothId = value;
        }

        public int Capacity
        {
            get => capacity;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                }
                capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => delicacyMenu;

        public IRepository<ICocktail> CocktailMenu => cocktailMenu;

        public double CurrentBill
        {
            get => currentBill;
            private set => currentBill = value;
        }

        public double Turnover
        {
            get => turnover;
            private set => turnover = value;
        }

        public bool IsReserved
        {
            get => isReserved;
            private set => isReserved = value;
        }

        public void ChangeStatus()
        {
            IsReserved = !IsReserved;
        }

        public void Charge()
        {
            Turnover += CurrentBill;

            CurrentBill = 0;
        }

        public void UpdateCurrentBill(double amount)
        {
            CurrentBill += amount;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Booth: {boothId}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Turnover: {Turnover:F2} lv");

            sb.AppendLine("-Cocktail menu:");

            foreach (var cocktail in cocktailMenu.Models)
            {
                sb.AppendLine($"--{cocktail.ToString()}");
            }

            sb.AppendLine("-Delicacy menu:");

            foreach (var delicacy in delicacyMenu.Models)
            {
                sb.AppendLine($"--{delicacy.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
