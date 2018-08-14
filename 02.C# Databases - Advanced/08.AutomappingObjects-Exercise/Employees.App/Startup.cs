using Employees.App.Core;
using Employees.App.Mapper;
using Employees.App.Service;

namespace Employees.App
{
    public class Startup
    {
        public static void Main()
        {
            //using (var dbContext = new EmployeesDbContext())
            //{
            //    dbContext.Database.EnsureDeleted();
            //    dbContext.Database.EnsureCreated();
            //}
            MapperInitializer.InitializeMapper();

            var serviceProvider = ServiceInitializer.ConfigureServices();
            var commandInterpreter = new CommandInterpreter(serviceProvider);

            var reader = new ConsoleReader();
            var writer = new ConsoleWriter();
            var engine = new Engine(reader,writer,commandInterpreter);

            engine.Run();
        }
    }
}
