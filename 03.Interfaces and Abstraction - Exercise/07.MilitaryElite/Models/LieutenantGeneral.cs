namespace MilitaryElite.Models
{
    using System.Collections.Generic;
    using System.Text;

    using Contracts;
    


    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        private readonly ICollection<IPrivate> privatesSoliders;
        public LieutenantGeneral(string id, string firstName, string lastName, decimal salary, ICollection<IPrivate> privateSoliders) : base(id, firstName, lastName, salary)
        {
            this.privatesSoliders = privateSoliders;
        }
        public IReadOnlyCollection<IPrivate> PrivateSoliders
           => (IReadOnlyCollection<IPrivate>)this.privatesSoliders;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:f2}");
            sb.AppendLine("Privates:");
            foreach (var item in PrivateSoliders)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
