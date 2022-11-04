namespace MilitaryElite.Models
{
    using System;

    using Contracts;
    
    public abstract class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
        private string corps;
        protected SpecialisedSoldier(string id, string firstName, string lastName, decimal salary, string corps) : base(id, firstName, lastName, salary)
        {
            Corps = corps;
        }

        
        public string Corps
        {
            get { return corps; }
            private set 
            {
                if (value != "Airforces" && value != "Marines")
                {
                    throw new ArgumentException();
                }
                corps = value;
            }
        }

    }
}
