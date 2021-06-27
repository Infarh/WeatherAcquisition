using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WeatherAcquisition.DAL.Entities;
using WeatherAcquisition.Interfaces.Base.Repositories;
using WeatherAcquisition.WebAPIClients.Repositories;

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
            services.AddHttpClient<IRepository<DataSource>, WebRepository<DataSource>>(
                client =>
                {
                    client.BaseAddress = new Uri($"{host.Configuration["WebAPI"]}/api/DataSources/"); // "/" в конце адреса обязательна!
                });
        }

        static async Task Main(string[] args)
        {
            using var host = Hosting;
            await host.StartAsync();

            var data_sources = Services.GetRequiredService<IRepository<DataSource>>();

            var start_count = await data_sources.GetCount();
            Console.WriteLine();
            Console.WriteLine(">>>   В начале было в репозитории элементов {0}", start_count);
            Console.WriteLine();

            //var sources = await data_sources.Get(3, 5);
            //foreach (var source in sources)
            //    Console.WriteLine($"{source.Id}: {source.Name} - {source.Description}");

            //var page = await data_sources.GetPage(4, 3);

            //var added_source = await data_sources.Add(
            //    new DataSource
            //    {
            //        Name = $"Source {DateTime.Now:HH-mm-ss}",
            //        Description = $"New source from web client at {DateTime.Now}"
            //    });

            var edited_item = await data_sources.Update(
                new DataSource
                {
                    Id = 6,
                    Name = $"Edited source at {DateTime.Now:HH-mm-ss}",
                    Description = "Edited description"
                });

            var end_count = await data_sources.GetCount();
            Console.WriteLine();
            Console.WriteLine(">>>   В конце репозитории элементов стало {0}", end_count);
            Console.WriteLine();

            var last_5_sources = await data_sources.Get(end_count - 5, 5);

            Console.WriteLine();
            foreach (var source in last_5_sources)
                Console.WriteLine($">>>   {source.Id}: {source.Name} - {source.Description}");
            Console.WriteLine();

            var first_items = (await data_sources.Get(0, 2)).ToArray();

            var deleted_item0 = await data_sources.DeleteById(first_items[0].Id);
            var deleted_item1 = await data_sources.Delete(first_items[1]);

            Console.WriteLine();
            Console.WriteLine(">>>   Completed");
            Console.ReadLine();
            await host.StopAsync();
        }
    }
}
