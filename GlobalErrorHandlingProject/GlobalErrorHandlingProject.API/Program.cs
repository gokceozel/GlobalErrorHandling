using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;

namespace GlobalErrorHandlingProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "/logs/");

            Log.Logger = new LoggerConfiguration()
               .WriteTo.Debug(Serilog.Events.LogEventLevel.Information)
               .WriteTo.File("logs.txt")
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });


    }
}
