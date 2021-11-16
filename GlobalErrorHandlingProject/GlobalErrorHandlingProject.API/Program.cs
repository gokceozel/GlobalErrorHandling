using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalErrorHandlingProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("project start");

            host.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
               WebHost.CreateDefaultBuilder(args)
             .UseStartup<Startup>().ConfigureLogging(logging =>
             {
                 logging.ClearProviders();
                 logging.AddDebug();
                 logging.AddConsole();
             });
    }
}
