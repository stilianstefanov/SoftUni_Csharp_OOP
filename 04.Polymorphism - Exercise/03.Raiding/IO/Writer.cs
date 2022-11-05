
namespace Raiding.IO
{
    using System;

    using Contracts;

    public class Writer : IWriter
    {
        public void Write(string value)
            => Console.Write(value);
        
        public void WriteLine(string value)
            => Console.WriteLine(value);
    }
}
