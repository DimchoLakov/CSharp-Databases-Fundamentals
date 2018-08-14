using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Employees.App.Contracts;

namespace Employees.App.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private const string Suffix = "Command";
        private const string NonExistingCommand = "Command not found!";
        private const string NotACommand = "This is not a command!";

        private readonly IServiceProvider _serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IExecutable GetCommand(IList<string> args)
        {
            string commandName = args[0];
            string fullCommandName = commandName + Suffix;

            var command = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == fullCommandName);

            if (command == null)
            {
                throw new InvalidOperationException(NonExistingCommand);
            }

            bool isAssignable = typeof(IExecutable).IsAssignableFrom(command);

            if (!isAssignable)
            {
                throw new ArgumentException(NotACommand);
            }

            List<string> paramList = args.Skip(1).ToList();

            ConstructorInfo constructor = command
                .GetConstructors()
                .First();

            var parameters = constructor
                .GetParameters()
                .Select(p => this._serviceProvider.GetService(p.ParameterType))
                .ToArray();

            parameters[0] = paramList; //first parameter is null because "args" do not exist in the service collection

            return (IExecutable)Activator.CreateInstance(command, parameters);
        }
    }
}
