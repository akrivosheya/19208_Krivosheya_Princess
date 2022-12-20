using Microsoft.EntityFrameworkCore;

namespace PrincessConsole
{
    class Program
    {
        private static readonly int AspirantsCount = 100;
        private static readonly int AttemptsCount = 100;
        private static readonly int MinArguments = 0;
        private static readonly string GeneratingArgument = "g";
        private static readonly string AverageArgument = "a";
        private static readonly string _connectionString = @"Server=localhost;Database=TestAppDB;User Id=postgres;Password=587963klop";

        static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IContenderGenerator, AttemptContenderGenerator>();
                    services.AddDbContextFactory<AspirantsContext>(optionsBuilder => optionsBuilder.UseNpgsql(_connectionString));
                    services.AddSingleton<AttemptNumber>(new AttemptNumber(){ Number=1 } );
                    services.AddSingleton<ControllerData>(new ControllerData());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .Build();
            host.StartAsync();
            Thread.Sleep(1000);
            if(args.Length > MinArguments)
            {
                if(Int32.TryParse(args[0], out int attemptNumber) && attemptNumber > 0 && attemptNumber <= AttemptsCount)
                {
                    var attemptsRunner = new AttemptsRunner();
                    attemptsRunner.runAttempt(args, attemptNumber, optionsBuilder => optionsBuilder.UseNpgsql(_connectionString));
                }
                else if(args[0].Equals(GeneratingArgument))
                {
                    var attemptsGenerator = new AttemptsGenerator(new RandomContenderGenerator());
                    var optionsBuilder = new DbContextOptionsBuilder<AspirantsContext>();
                    optionsBuilder.UseNpgsql(_connectionString);
                    attemptsGenerator.generateAttempts(AttemptsCount, AspirantsCount, optionsBuilder.Options);
                }
                else if(args[0].Equals(AverageArgument))
                {
                    var optionsBuilder = new DbContextOptionsBuilder<AspirantsContext>();
                    optionsBuilder.UseNpgsql(_connectionString);
                    using(var context = new AspirantsContext(optionsBuilder.Options))
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
                var attemptsRunner = new AttemptsRunner();
                var optionsBuilder = new DbContextOptionsBuilder<AspirantsContext>();
                optionsBuilder.UseNpgsql(_connectionString);
                attemptsRunner.runAllAttempts(args, optionsBuilder, optionsBuilder => optionsBuilder.UseNpgsql(_connectionString));
            }
            host.WaitForShutdown();
        }
    }
}
