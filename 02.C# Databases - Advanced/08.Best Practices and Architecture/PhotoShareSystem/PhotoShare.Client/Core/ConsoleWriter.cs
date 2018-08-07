using System;
using PhotoShare.Client.Core.Contracts;

namespace PhotoShare.Client.Core
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
