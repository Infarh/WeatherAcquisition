using System;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherAcqusition.BalzorUI.Infrastructure
{
    internal static class ServicesExtensions
    {
        public static IWebAssemblyHostEnvironment GetEnvironment(this IServiceProvider services) => services.GetRequiredService<IWebAssemblyHostEnvironment>();

        public static IHttpClientBuilder AddApi<TInterface, TImplementation>(this IServiceCollection services, string Address) 
            where TInterface : class where TImplementation : class, TInterface => services
           .AddHttpClient<TInterface, TImplementation>((host, client) => client.BaseAddress = new($"{host.GetEnvironment().BaseAddress}api/{Address}"));
    }
}
