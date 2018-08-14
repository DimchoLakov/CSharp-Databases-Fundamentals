namespace BusTicket.Client.Core.Contracts
{
    public interface ICommandInterpreter
    {
        string Read(string[] args);
    }
}
