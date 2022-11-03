using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models.Contracts
{
    public interface ISpecialisedSoldier : IPrivate
    {
        public string Corps { get; }
    }
}
