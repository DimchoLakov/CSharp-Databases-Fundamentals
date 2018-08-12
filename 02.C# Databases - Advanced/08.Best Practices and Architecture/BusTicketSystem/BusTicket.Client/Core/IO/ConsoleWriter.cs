using System;
using BusTicket.Client.Core.Contracts;

namespace BusTicket.Client.Core.IO
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string input)
        {
            Console.WriteLine(input);
        }

        public void Write(string input)
        {
            Console.Write(input);
        }
    }
}
