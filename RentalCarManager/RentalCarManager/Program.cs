using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RentalCarManager.Data;
using RentalCarManager.Handlers;
using RentalCarManager.Managers;
using RentalCarManager.Services;
using Spectre.Console;
using System.Text;

namespace RentalCarManager
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            IHost? host = null;
            await AnsiConsole.Status()
           .Spinner(Spinner.Known.Dots)
           .StartAsync("Configuring Services...", async ctx =>
           {
               host = CreateHostBuild(args);
               ctx.Status("Checking database...");
               VehicleContext context = host.Services.GetRequiredService<VehicleContext>();
               if (!context.Database.CanConnect())
               {
                   ctx.Status("Creating database...");
                   CreateDbIfNotExists(context);
               }
               ctx.Status("Fetching database...");
           });
            host.Services.GetService<ConsoleManager>().Run();
            Console.ReadLine();
        }
        private static IHost CreateHostBuild(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
             {
                 logging.ClearProviders();
                 logging.AddConsole();
                 logging.AddDebug();
                 logging.AddEventSourceLogger();
                 logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
             })
              .ConfigureServices((context, services) =>
             {
                 services.AddDbContext<VehicleContext>(options => options.UseSqlite($"Data Source = RentalCarManager.db"));
                 services.AddTransient<VehicleService>();
                 services.AddTransient<OutputHandler>();
                 services.AddSingleton<ConsoleManager>();
             }).Build();
        }
        private static void CreateDbIfNotExists(VehicleContext context)
        {
            context.Database.EnsureCreated();
            DbInitializer.Initialize(context);
        }
    }
}