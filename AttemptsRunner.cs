using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace PrincessConsole
{
    public class AttemptsRunner
    {
        public void runAttempt(string[] args, int attemptNumber, Action<DbContextOptionsBuilder> optionsBuilding)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Princess>();
                    services.AddScoped<Hall>();
                    services.AddScoped<Friend>();
                    services.AddTransient<IContenderGenerator, AttemptContenderGenerator>();
                    services.AddTransient<IResultWriter, DbResultWriter>();
                    services.AddDbContextFactory<AspirantsContext>(optionsBuilding);
                    services.AddSingleton<AttemptNumber>(new AttemptNumber(){ Number=attemptNumber } );
                })
                .Build();
            host.StartAsync();
            host.WaitForShutdown();
        }

        
        public void runAllAttempts(string[] args, DbContextOptionsBuilder<AspirantsContext> optionsBuilder, Action<DbContextOptionsBuilder> optionsBuilding)
        {
            using(var context = new AspirantsContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
                int attemptsCount = context.Attempts.Count();
                for(int i = 0; i < attemptsCount; ++i)
                {
                    var attempt = context.Attempts.Find(i + 1)!;
                    if(attempt.Happiness == null)
                    {
                        runAttempt(args, i + 1, optionsBuilding);
                    }
                }
            }
        }
    }
}