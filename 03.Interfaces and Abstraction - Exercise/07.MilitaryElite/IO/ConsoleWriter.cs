﻿


namespace MilitaryElite.IO
{
    using System;

    using Contracts;

    public class ConsoleWriter : IWriter
    {
        public void Write(string value)
             => Console.Write(value);

        public void WriteLine(string value)
             => Console.WriteLine(value);
    }
}
