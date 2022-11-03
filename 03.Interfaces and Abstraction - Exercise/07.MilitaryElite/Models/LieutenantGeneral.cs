using MilitaryElite.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models
{
    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        
        public LieutenantGeneral(string id, string firstName, string lastName, decimal salary, List<IPrivate> privateSoliders) : base(id, firstName, lastName, salary)
        {
            this.PrivateSoliders = privateSoliders;
        }
         public List<IPrivate> PrivateSoliders { get; private set; }

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
