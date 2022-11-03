using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models.Contracts
{
    public interface IPrivate : ISolder
    {
        public decimal Salary { get; }
    }
}
