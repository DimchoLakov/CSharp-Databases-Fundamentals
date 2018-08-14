using System.Collections.Generic;
using Employees.App.Contracts;

namespace Employees.App.Core.Commands
{
    public abstract class Command : IExecutable
    {
        protected Command(IList<string> args)
        {
            this.Args = args;
        }

        public IList<string> Args { get; }

        public abstract string Execute();
    }
}
