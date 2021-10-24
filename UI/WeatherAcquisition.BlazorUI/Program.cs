using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherAcquisition.BlazorUI.Infrastructure.Extensions;
using WeatherAcquisition.Domain.Base;
using WeatherAcquisition.Interfaces.Base.Repositories;
using WeatherAcquisition.WebAPIClients.Repositories;

namespace WeatherAcquisition.BlazorUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var services = builder.Services;
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //services.AddHttpClient<IRepository<DataSourceInfo>, WebRepository<DataSourceInfo>>(
            //    (host, client) => client.BaseAddress = new(host.GetRequiredService<IWebAssemblyHostEnvironment>().BaseAddress + "api/SourcesRepository"));

            services.AddApi<IRepository<DataSourceInfo>, WebRepository<DataSourceInfo>>("api/SourcesRepository");

            await builder.Build().RunAsync();
        }
    }
}
