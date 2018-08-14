using System;
using BusTicket.Client.Core.Contracts;

namespace BusTicket.Client.Core.Commands
{
    public class ExitCommand : IExecutable
    {
        private const string ExitMessage = "Bye, bye!";
        private readonly IWriter _writer;

        public ExitCommand(IWriter writer)
        {
            this._writer = writer;
        }

        public string Execute(string[] args)
        {
            this._writer.WriteLine(ExitMessage);

            Environment.Exit(0);

            return null;
        }
    }
}
