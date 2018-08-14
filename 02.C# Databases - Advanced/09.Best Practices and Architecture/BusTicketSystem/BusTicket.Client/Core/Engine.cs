using System;
using System.Data.SqlClient;
using BusTicket.Client.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BusTicket.Client.Core
{
    public class Engine : IEngine
    {
        private const string WelcomeMessage = "Please enter a command: ";
        private const string InvalidInputMessage = "Invalid input!";

        private readonly IServiceProvider _serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public void Run()
        {
            var reader = this._serviceProvider.GetService<IReader>();
            var writer = this._serviceProvider.GetService<IWriter>();
            var commandInterpreter = this._serviceProvider.GetService<ICommandInterpreter>();

            while (true)
            {
                try
                {
                    writer.WriteLine(WelcomeMessage);
                    string input = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        writer.WriteLine(InvalidInputMessage);
                        continue;
                    }

                    string[] args = input
                        .Split(new[] { ' ', ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                    var result = commandInterpreter.Read(args);

                    writer.WriteLine(result);
                }
                catch (Exception exception) when (exception is SqlException ||
                                                  exception is ArgumentException ||
                                                  exception is InvalidOperationException ||
                                                  exception is IndexOutOfRangeException)
                {
                    writer.WriteLine(exception.Message);
                }
            }
        }
    }
}
