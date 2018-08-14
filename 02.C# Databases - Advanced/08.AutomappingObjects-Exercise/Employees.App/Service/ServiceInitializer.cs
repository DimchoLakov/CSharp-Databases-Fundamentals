using System;
using AutoMapper;
using Employees.App.Contracts;
using Employees.App.Core;
using Employees.Data;
using Employees.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Employees.App.Service
{
    public class ServiceInitializer
    {
        public static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesDbContext>(options =>
            {
                options.UseSqlServer(DbContextConfig.ConnectionString);
            });

            serviceCollection.AddTransient<IWriter, ConsoleWriter>();
            serviceCollection.AddTransient<IReader, ConsoleReader>();

            serviceCollection.AddAutoMapper();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
