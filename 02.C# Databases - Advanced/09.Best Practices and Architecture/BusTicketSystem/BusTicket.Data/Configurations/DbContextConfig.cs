namespace BusTicket.Data.Configurations
{
    public class DbContextConfig
    {
        public const string ConnectionString = @"Server=.;" +
                                               "Database=BusTicketDb;" +
                                               "Integrated Security=True;";
    }
}
