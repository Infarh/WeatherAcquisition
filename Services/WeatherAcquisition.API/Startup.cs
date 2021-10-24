using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WeatherAcquisition.API.Data;
using WeatherAcquisition.DAL.Context;
using WeatherAcquisition.DAL.Repositories;
using WeatherAcquisition.Interfaces.Base.Repositories;

namespace WeatherAcquisition.API
{
    public record Startup(IConfiguration Сonfiguration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataDB>(
                opt => opt
                   .UseSqlServer(
                        Сonfiguration.GetConnectionString("Data"),
                        o => o.MigrationsAssembly("WeatherAcquisition.DAL.SqlServer")));
            services.AddTransient<DataDBInitializer>();

            //services.AddScoped<IRepository<DataSource>, DbRepository<DataSource>>();
            //services.AddScoped<IRepository<DataValue>, DbRepository<DataValue>>();
            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped(typeof(INamedRepository<>), typeof(DbNamedRepository<>));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherAcquisition.API", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataDBInitializer db)
        {
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherAcquisition.API v1"));
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
