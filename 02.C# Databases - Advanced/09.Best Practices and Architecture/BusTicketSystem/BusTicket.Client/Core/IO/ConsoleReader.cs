using System;
using BusTicket.Client.Core.Contracts;

namespace BusTicket.Client.Core.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
