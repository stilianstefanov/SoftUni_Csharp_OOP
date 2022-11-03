using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models.Contracts
{
    public interface ISolder
    {
        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
