using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models.Contracts
{
    public interface ILieutenantGeneral : IPrivate
    {
        public List<IPrivate> PrivateSoliders { get; }
    }
}
