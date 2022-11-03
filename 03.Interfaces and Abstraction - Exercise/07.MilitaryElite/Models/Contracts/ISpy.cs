using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models.Contracts
{
    public interface ISpy : ISolder
    {
        public int CodeNumber { get; }
    }
}
