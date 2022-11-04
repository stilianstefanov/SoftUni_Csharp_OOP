﻿namespace MilitaryElite.Models.Contracts
{
    using System.Collections.Generic;


    public interface ILieutenantGeneral : IPrivate
    {
        public IReadOnlyCollection<IPrivate> PrivateSoliders { get; }
    }
}
