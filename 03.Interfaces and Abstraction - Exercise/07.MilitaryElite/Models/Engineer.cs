namespace MilitaryElite.Models
{
    using System.Collections.Generic;
    using System.Text;

    using Contracts;


    public class Engineer : SpecialisedSoldier, IEngineer
    {
        private readonly ICollection<Repair> repairs;

        public Engineer(string id, string firstName, string lastName, decimal salary, string corps, ICollection<Repair> repairs) : base(id, firstName, lastName, salary, corps)
        {
            this.repairs = repairs;
        }

        public IReadOnlyCollection<Repair> Repairs 
            => (IReadOnlyCollection<Repair>)this.repairs;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:f2}");
            sb.AppendLine($"Corps: {Corps}");
            sb.AppendLine("Repairs:");
            foreach (var item in Repairs)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
