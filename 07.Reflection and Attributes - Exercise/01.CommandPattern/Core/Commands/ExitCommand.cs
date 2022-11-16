namespace CommandPattern.Core.Commands
{
    using Contracts;
    using System;

    internal class ExitCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Environment.Exit(0);

            return null;
        }
    }
}
