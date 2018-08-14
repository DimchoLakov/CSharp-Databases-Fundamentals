using System;
using Employees.App.Contracts;

namespace Employees.App.Core
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
