using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PrincessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Princess>();
                    services.AddScoped<Hall>();
                    services.AddScoped<Friend>();
                    services.AddScoped<ContenderGenerator>();
                    services.AddTransient<ResultWriter>();
                })
                .Build();
            host.Run();
        }
    }
}
