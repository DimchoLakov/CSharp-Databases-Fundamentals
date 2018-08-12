using System;
using BusTicket.Client.Core;
using BusTicket.Client.Core.Contracts;
using BusTicket.Client.Core.IO;
using BusTicket.Data;
using BusTicket.Services;
using BusTicket.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BusTicket.Client
{
    public class Startup
    {
        public static void Main()
        {
            var services = ConfigureServices();

            DatabaseInitializer.InitializeDatabase(services.GetService<BusTicketContext>());

            var engine = new Engine(services);

            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<BusTicketContext>();
            services.AddTransient<IReader, ConsoleReader>();
            services.AddTransient<IWriter, ConsoleWriter>();
            services.AddTransient<ICommandInterpreter, CommandInterpreter>();
            services.AddTransient<IBankAccountService, BankAccountService>();
            services.AddTransient<IBusCompanyService, BusCompanyService>();
            services.AddTransient<IBusStationService, BusStationService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ITownService, TownService>();
            services.AddTransient<ITripService, TripService>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
