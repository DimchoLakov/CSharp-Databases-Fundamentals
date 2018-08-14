using System;
using Employees.App.Contracts;

namespace Employees.App.Core
{
    public class Engine : IEngine
    {
        private const string CommandInput = "Please enter command: ";

        private readonly IReader _consoleReader;
        private readonly IWriter _consoleWriter;
        private readonly ICommandInterpreter _commandInterpreter;

        public Engine(IReader reader, IWriter writer, ICommandInterpreter commandInterpreter)
        {
            this._consoleReader = reader;
            this._consoleWriter = writer;
            this._commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            while (true)
            {
                this._consoleWriter.WriteLine(CommandInput);
                var commandAsStr = this._consoleReader.ReadLine();
                var args = commandAsStr.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    var command = this._commandInterpreter.GetCommand(args);

                    string result = command.Execute();
                    this._consoleWriter.WriteLine(result);
                }
                catch (Exception e)
                {
                    this._consoleWriter.WriteLine(e.Message);
                }
            }
        }
    }
}
