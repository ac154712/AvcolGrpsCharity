using AvcolGrpsCharity.Areas.Identity.Data; 
using AvcolGrpsCharity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting; 
namespace AvcolGrpsCharity
{
    public class Startup
    {
        public Startup(IConfiguration configuration) // Constructor to initialize the configuration
        {
            Configuration = configuration; // assigning the injected configuration to the local Configuration property
        }

        public IConfiguration Configuration { get; } // a property to hold the configuration

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // gets the connection string from the configuration
            var connectionString = Configuration.GetConnectionString("AvcolGrpsCharityDbContextConnection")
                ?? throw new InvalidOperationException("Connection string 'AvcolGrpsCharityDbContextConnection' not found.");

            // adds the database context to the service container with SQL Server provider and retry on failure
            services.AddDbContext<AvcolGrpsCharityDbContext>(options =>
                options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure()));

            // adds default identity services with entity framework stores
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AvcolGrpsCharityDbContext>();

            // adds controllers with views and Razor Pages services to the container
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment()) // checks if the environment is not development
            {
                app.UseExceptionHandler("/Home/Error"); // uses exception handler for production environment
                app.UseHsts(); // uses HTTP Strict Transport Security (HSTS)
            }

            app.UseHttpsRedirection(); // redirects HTTP requests to HTTPS
            app.UseStaticFiles(); // enables serving static files

            app.UseRouting(); // enables routing middleware

            app.UseAuthorization(); // enables authorization middleware

            app.UseEndpoints(endpoints => // Configure the endpoints
            {
                endpoints.MapRazorPages(); // Map Razor Pages endpoints
                endpoints.MapControllerRoute( // Map default controller route
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
