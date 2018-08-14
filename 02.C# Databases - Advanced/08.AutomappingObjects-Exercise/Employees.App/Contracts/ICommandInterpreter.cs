using System.Collections.Generic;

namespace Employees.App.Contracts
{
    public interface ICommandInterpreter
    {
        IExecutable GetCommand(IList<string> args);
    }
}
