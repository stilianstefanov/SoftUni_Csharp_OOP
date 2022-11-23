namespace PlanetWars.Models.MilitaryUnits
{
    using System;

    using Contracts;
    using Utilities.Messages;
  

    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private double cost;
        private int enduranceLevel;

        protected MilitaryUnit(double cost)
        {
            this.Cost = cost;
            this.EnduranceLevel = 1;
        }

        public double Cost
        {
            get => cost; 
            private set
            {
                cost = value;
            }
        }

        public int EnduranceLevel
        {
            get => enduranceLevel;
            private set
            {
                enduranceLevel = value;
            }
        }

        public void IncreaseEndurance()
        {
            if (EnduranceLevel == 20)
            {
                throw new ArgumentException(ExceptionMessages.EnduranceLevelExceeded);
            }

            EnduranceLevel++;
        }     
    }
}
