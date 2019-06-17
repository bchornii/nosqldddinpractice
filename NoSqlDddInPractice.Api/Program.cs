﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace NoSqlDddInPractice.Api
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .Enrich.WithProperty("ApplicationContext", AppName)
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .WriteTo.File($"Logs/{AppName}.txt",
                   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                   levelSwitch: new Serilog.Core.LoggingLevelSwitch { MinimumLevel = LogEventLevel.Error },
                   rollingInterval: RollingInterval.Day)
               .CreateLogger();

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);
                var host = CreateWebHostBuilder(args).Build();

                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                host.Run();

                return 0;
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseSerilog();
    }
}
