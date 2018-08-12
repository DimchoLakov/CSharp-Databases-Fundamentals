using BusTicket.Data;
using Microsoft.EntityFrameworkCore;

namespace BusTicket.Client
{
    public class DatabaseInitializer
    {
        public static void InitializeDatabase(BusTicketContext dbContext)
        {
            dbContext.Database.Migrate();
        }
    }
}
