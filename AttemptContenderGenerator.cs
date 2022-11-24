using Microsoft.EntityFrameworkCore;

namespace PrincessConsole
{
    public class AttemptContenderGenerator : IContenderGenerator
    {
        private IDbContextFactory<AspirantsContext> _contextFactory;
        private int _attemptNumber;

        public AttemptContenderGenerator(IDbContextFactory<AspirantsContext> contextFactory, AttemptNumber attemptNumber)
        {
            _contextFactory = contextFactory;
            _attemptNumber = attemptNumber.Number;
        }

        public void GenerateContenders(string[] queue, Dictionary<string, Aspirant> aspirants)
        {
            using (var context = _contextFactory.CreateDbContext()) 
            {
                context.Database.EnsureCreated();
                var attempt = context.Attempts.Find(_attemptNumber);
                var order = attempt!.Aspirants!;

                for(int i = 0; i < queue.Length; ++i)
                {
                    var aspirant = context.Aspirants.Find(order[i])!;
                    aspirants.Add(aspirant.Name!, new Aspirant { Name=aspirant.Name!, Quality=aspirant.Quality, IsWasted=false });
                    queue[i] = aspirant.Name!;
                }
            }
        }
    }
}