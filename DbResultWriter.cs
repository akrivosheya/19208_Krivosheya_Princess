using Microsoft.EntityFrameworkCore;

namespace PrincessConsole
{
    public class DbResultWriter : IResultWriter
    {
        private const string NoGroom = "";
        private const string UnknownAspirant = "Unknown Aspirant";
        private const int MinQuality = 50;
        private const int LonelinessHappinessPoints = 10;
        private const int UnhappinessPoints = 0;
        private IDbContextFactory<AspirantsContext> _contextFactory;
        private int _attemptNumber;

        public DbResultWriter(IDbContextFactory<AspirantsContext> contextFactory, AttemptNumber attemptNumber)
        {
            _contextFactory = contextFactory;
            _attemptNumber = attemptNumber.Number;
        }

        public void WriteResult(Hall hall, string groom)
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                context.Database.EnsureCreated();
                var attempt = context.Attempts.Find(_attemptNumber)!;
                if(groom == NoGroom)
                {
                    attempt.Happiness = LonelinessHappinessPoints;
                }
                else
                {
                    int? groomQuality = hall[groom]?.Quality;
                    if(groomQuality == null)
                    {
                    }
                    else if(groomQuality <= MinQuality)
                    {
                        attempt.Happiness =  UnhappinessPoints;
                    }
                    else
                    {
                        attempt.Happiness =  groomQuality;
                    }
                }
                context.SaveChanges();
            }
        }
    }
}