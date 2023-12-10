using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Sockets;

namespace ArchLab6
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Configuration.AddJsonFile("C:\\Users\\Андрей Лузгин\\source\\repos\\ArchLab6\\appsettings.json");

            builder.Services
                .AddDbContext<Lab6DbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")))
                .AddSingleton<ConsoleView>()
                .AddSingleton<Scraper>()
                .AddScoped<Controller>()
                .AddHostedService<ControllerHost>();


            var host = builder.Build();

            await host.RunAsync();
        }
    }
}