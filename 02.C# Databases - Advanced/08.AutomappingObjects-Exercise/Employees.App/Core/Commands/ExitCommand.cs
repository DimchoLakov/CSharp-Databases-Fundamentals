using System;
using System.Collections.Generic;
using Employees.App.Contracts;

namespace Employees.App.Core.Commands
{
    public class ExitCommand : Command
    {
        private readonly IWriter _writer;

        public ExitCommand(IList<string> args, IWriter writer) : base(args)
        {
            this._writer = writer;
        }

        public override string Execute()
        {
            this._writer.WriteLine("Bye, bye!");

            Environment.Exit(0);

            return "Bye, bye!";
        }
    }
}
