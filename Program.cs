using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PrincessConsole
{
    class Program
    {
        private static int AspirantsCount = 100;
        private static int AttemptsCount = 100;
        private static int MinArguments = 0;
        private static string GeneratingArgument = "g";
        private static string AverageArgument = "a";

        static void Main(string[] args)
        {
            if(args.Length > MinArguments)
            {
                if(Int32.TryParse(args[0], out int attemptNumber) && attemptNumber > 0 && attemptNumber <= AttemptsCount)
                {
                    runAttempt(args, attemptNumber);
                }
                else if(args[0].Equals(GeneratingArgument))
                {
                    var attemptsGenerator = new AttemptsGenerator();
                    attemptsGenerator.generateAttempts(AttemptsCount, AspirantsCount);
                }
                else if(args[0].Equals(AverageArgument))
                {
                    using(var context = new AspirantsContext())
                    {
                        context.Database.EnsureCreated();
                        int attemptsCount = context.Attempts.Count();
                        int countedAttempts = 0;
                        int allHappiness = 0;
                        for(int i = 0; i < attemptsCount; ++i)
                        {
                            var attempt = context.Attempts.Find(i + 1)!;
                            if(attempt.Happiness != null)
                            {
                                allHappiness += (int)attempt!.Happiness!;
                                ++countedAttempts;
                            }
                        }
                        System.Console.WriteLine("Average happiness: " + (float)allHappiness / countedAttempts);
                    }
                }
                else
                {
                    System.Console.WriteLine("Wrong argument: {0} isn't integer between 0 and {1}, generating argument {2} or average argument {3}",
                                            args[0], AttemptsCount, GeneratingArgument, AverageArgument);
                }
            }
            else
            {
                using(var context = new AspirantsContext())
                {
                    context.Database.EnsureCreated();
                    int attemptsCount = context.Attempts.Count();
                    for(int i = 0; i < attemptsCount; ++i)
                    {
                        var attempt = context.Attempts.Find(i + 1)!;
                        if(attempt.Happiness == null)
                        {
                            runAttempt(args, i + 1);
                        }
                    }
                }
            }
            /*IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Princess>();
                    services.AddScoped<Hall>();
                    services.AddScoped<Friend>();
                    services.AddTransient<IContenderGenerator, RandomContenderGenerator>();
                    services.AddTransient<IResultWriter, FileResultWriter>();
                })
                .Build();
            host.StartAsync();
            host.WaitForShutdown();*/
        }

        private static void runAttempt(string[] args, int attemptNumber)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Princess>();
                    services.AddScoped<Hall>();
                    services.AddScoped<Friend>();
                    services.AddTransient<IContenderGenerator, AttemptContenderGenerator>();
                    services.AddTransient<IResultWriter, DbResultWriter>();
                    services.AddDbContextFactory<AspirantsContext>();
                    services.AddSingleton<AttemptNumber>(new AttemptNumber(){ Number=attemptNumber } );
                })
                .Build();
            host.StartAsync();
            host.WaitForShutdown();
        }
    }
}
