using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarrisContactManager.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HarrisContactManager
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<HarrisDbContext>();
                    DInitializer.Initialize(context);

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<HarrisDbContext>();
                    logger.logError(ex, "An Error has occured while creating the database");
                }

            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
