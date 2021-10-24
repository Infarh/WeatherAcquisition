using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WeatherAcquisition.Domain.Base;
using WeatherAcquisition.Interfaces.Base.Repositories;
using WeatherAcquisition.WebAPIClients.Repositories;
using WeatherAcqusition.BalzorUI.Infrastructure;

namespace WeatherAcqusition.BalzorUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var services = builder.Services;
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(sp.GetEnvironment().BaseAddress) });
            services.AddApi<IRepository<DataSourceInfo>, WebRepository<DataSourceInfo>>("SourcesRepository");

            await builder.Build().RunAsync();
        }
    }
}
