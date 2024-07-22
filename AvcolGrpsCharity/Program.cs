using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using Microsoft.Extensions.Logging; 
using AvcolGrpsCharity.Data; 
using System; 
using AvcolGrpsCharity.Areas.Identity.Data; 

namespace AvcolGrpsCharity
{
    public class Program
    {
        public static void Main(string[] args) 
        {
            var host = CreateHostBuilder(args).Build(); // Builds the host

            CreateDbIfNotExists(host); // Ensures the database is created if it doesn't exist

            host.Run(); // runs the host
        }

        private static void CreateDbIfNotExists(IHost host) // Method to ensure database creation
        {
            using (var scope = host.Services.CreateScope()) // creates a scope for retrieving services
            {
                var services = scope.ServiceProvider; // gets the service provider
                try
                {
                    var context = services.GetRequiredService<AvcolGrpsCharityDbContext>(); // gets the database context service
                    DbInitializer.Initialize(context); // initializes the database
                }
                catch (Exception ex) // Catch any exceptions during database initialization
                {
                    var logger = services.GetRequiredService<ILogger<Program>>(); // gets the logging service
                    logger.LogError(ex, "An error occurred creating the DB."); // Logs the error
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => // Method to create the host builder
            Host.CreateDefaultBuilder(args) // creates the default host builder
                .ConfigureWebHostDefaults(webBuilder => // configures the web host defaults
                {
                    webBuilder.UseStartup<Startup>(); // specifies the startup class to use
                });
    }
}
