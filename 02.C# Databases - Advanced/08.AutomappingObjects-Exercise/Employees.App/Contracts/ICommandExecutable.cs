using System.Collections.Generic;

namespace Employees.App.Contracts
{
    public interface IExecutable
    {
        IList<string> Args { get; }

        string Execute();
    }
}
