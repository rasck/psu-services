using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PSUBookingService.DAL;
using Microsoft.EntityFrameworkCore;

namespace PSUBookingService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           

            // Add framework services.
            services.AddMvc();
            //add database service
            // the default connection string will be compiled so that we can hide the details of how to connect to our DB
            string defaultConnection = "Data Source=dm.sof60.dk,1433;Initial Catalog=PSUNCBooking;Integrated Security=False;User ID=psumaster;Password=solur123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            // But if anyone sets a ConnectionString in the environment variable (e.g. using docker) they can use their own database
            string connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? defaultConnection;

            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            //add the repository as transient, each request has its own
            services.AddTransient<IBookingRepository, BookingRepository>();

            //ensure database
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var context = new DataContext(optionsBuilder.Options);
            context.Database.EnsureCreated();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            //ensure that the database is created c:\user\<username>\psubooking.mdf
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
