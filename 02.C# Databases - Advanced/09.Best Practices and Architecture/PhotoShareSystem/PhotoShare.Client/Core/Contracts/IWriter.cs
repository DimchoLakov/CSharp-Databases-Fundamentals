namespace PhotoShare.Client.Core.Contracts
{
    public interface IWriter
    {
        void WriteLine(string input);

        void Write(string input);
    }
}
