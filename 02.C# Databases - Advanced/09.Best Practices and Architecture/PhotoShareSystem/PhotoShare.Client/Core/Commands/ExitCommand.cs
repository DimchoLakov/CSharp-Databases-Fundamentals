namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;

    public class ExitCommand : ICommand
    {
        private const string ExitMessage = "Bye, bye!";

        private readonly IWriter writer;

        public ExitCommand(IWriter writer)
        {
            this.writer = writer;
        }

        public string Execute(string[] data)
        {
            this.writer.WriteLine(ExitMessage);
            Environment.Exit(0);

            return null;
        }
    }
}
