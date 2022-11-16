namespace CommandPattern.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Contracts;
   

    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            string[] tokens = args.Split();

            string commandName = tokens[0];
            string[] value = tokens.Skip(1).ToArray();

            Type commandType = Assembly.GetCallingAssembly().GetTypes()
                .Where(t => t.Name.StartsWith(commandName))
                .FirstOrDefault();

            ICommand command = Activator.CreateInstance(commandType) as ICommand;


            return command.Execute(value);
        }
    }
}
