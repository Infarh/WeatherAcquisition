using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WeatherAcquisition.ConsoleUI
{
    class Program
    {
        private static IHost __Hosting;

        public static IHost Hosting => __Hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;

        public static IHostBuilder CreateHostBuilder(string[] Args) => Host
           .CreateDefaultBuilder(Args)
           .ConfigureServices(ConfigureServices)
        ;

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {

        }

        static async Task Main(string[] args)
        {
            using var host = Hosting;
            await host.StartAsync();



            Console.WriteLine();
            Console.WriteLine("Completed");
            Console.ReadLine();
            await host.StopAsync();
        }
    }
}
