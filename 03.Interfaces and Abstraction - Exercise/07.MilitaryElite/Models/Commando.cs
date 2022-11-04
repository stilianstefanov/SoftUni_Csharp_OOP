namespace MilitaryElite.Models
{
    using System.Collections.Generic;
    using System.Text;

    using Contracts;
    

    public class Commando : SpecialisedSoldier, ICommando
    {
        private readonly ICollection<Mission> missions;
        public Commando(string id, string firstName, string lastName, decimal salary, string corps, ICollection<Mission> missions) : base(id, firstName, lastName, salary, corps)
        {
            this.missions = missions;
        }
        public IReadOnlyCollection<Mission> Missions
            => (IReadOnlyCollection<Mission>)this.missions;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:f2}");
            sb.AppendLine($"Corps: {Corps}");
            sb.AppendLine("Missions:");
            foreach (var item in Missions)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
