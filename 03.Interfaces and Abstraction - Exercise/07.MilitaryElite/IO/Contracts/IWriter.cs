using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.IO.Contracts
{
    public interface IWriter
    {
        void Write(string value);

        void WriteLine(string value);
    }
}
