﻿using Library.Data.NewFolder;
using Library.Data;
using Library.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Library.Model.Models;
using Library.Service.Interfaces;
using Library.Service.Logging;

namespace LibraryManagement.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
           services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();



        // using AddDbContext method to register RepositoryContext with DI container
        // In our case we are specifing that the context should use SQL server as a database provider
        public static void ConfigureSqlContext(this IServiceCollection services,
                        IConfiguration configuration) =>
                        services.AddDbContext<RepositoryContext>(opts =>
                        opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
                        .EnableSensitiveDataLogging());


        // configuring Identity
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<Employee, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders(); //default token provider for reseting passwords, emails and etc.

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(5); // Token is valid for 5 minutes
            });
        }


        public static void AddMailjetConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailjetSettings>(configuration.GetSection("MailjetSettings"));
        }

        public static void AddVonageConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<VonageSettings>(configuration.GetSection("VonageSettings"));
        }


        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>(); 
        }

    
      



    }
}
