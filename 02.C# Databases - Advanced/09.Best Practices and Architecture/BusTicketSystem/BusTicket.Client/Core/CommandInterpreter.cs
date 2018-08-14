using System;
using System.Linq;
using System.Reflection;
using BusTicket.Client.Core.Contracts;

namespace BusTicket.Client.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private const string Suffix = "Command";
        private const string InvalidCommand = "Command \"{0}\" is not valid!";
        private const string NotACommand = "This is not a command!";

        private readonly IServiceProvider _serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public string Read(string[] args)
        {
            string commandName = args[0];
            string fullCommandName = commandName + Suffix;

            var commandType = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == fullCommandName);

            if (commandType == null)
            {
                throw new InvalidOperationException(string.Format(InvalidCommand, commandName));
            }

            var isAssignable = typeof(IExecutable).IsAssignableFrom(commandType);

            if (!isAssignable)
            {
                throw new ArgumentException(NotACommand);
            }

            var constructor = commandType
                .GetConstructors()
                .First();

            var constructorParameters = constructor
                .GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();

            var services = constructorParameters
                .Select(x => this._serviceProvider.GetService(x))
                .ToArray();

            var command = (IExecutable)constructor.Invoke(services);

            string[] tokens = args
                .Skip(1)
                .ToArray();

            var result = command.Execute(tokens);

            return result;
        }
    }
}
