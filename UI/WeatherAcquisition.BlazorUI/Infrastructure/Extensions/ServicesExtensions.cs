using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherAcquisition.BlazorUI.Infrastructure.Extensions
{
    internal static class ServicesExtensions
    {
        public static IHttpClientBuilder AddApi<IInterface, IClient>(this IServiceCollection services, string Address)
            where IInterface : class where IClient : class, IInterface => services
           .AddHttpClient<IInterface, IClient>(
                (host, client) => client.BaseAddress = new($"{host.GetRequiredService<IWebAssemblyHostEnvironment>().BaseAddress}{Address}"));
    }
}
