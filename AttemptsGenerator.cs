namespace PrincessConsole
{
    public class AttemptsGenerator
    {
        public void generateAttempts(int AttemptsCount, int AspirantsCount)
        {
            using(var context = new AspirantsContext())
            {
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var queue = new string[AspirantsCount];
                var aspirants = new Dictionary<string, Aspirant>();
                var aspirantsData = new Dictionary<string, AspirantData>();
                var generator = new RandomContenderGenerator();
                generator.GenerateContenders(queue, aspirants);
                context.Aspirants.RemoveRange(context.Aspirants.ToArray());
                context.Attempts.RemoveRange(context.Attempts.ToArray());
                for(int i = 0; i < queue.Length; ++i)
                {
                    var aspirant = new AspirantData() { Id=i, Name=queue[i], Quality=aspirants[queue[i]].Quality };
                    context.Aspirants.Add(aspirant);
                    aspirantsData.Add(aspirant.Name, aspirant);
                }
                for(int i = 0; i < AttemptsCount; ++i)
                {
                    var aspirantsInAttempt = new List<int>();
                    foreach(string aspirantName in queue)
                    {
                        aspirantsInAttempt.Add(aspirantsData[aspirantName].Id);
                    }
                    var attempt = new Attempt() { Id=(i + 1), Aspirants=aspirantsInAttempt };
                    context.Attempts.Add(attempt);
                    ShuffleArray(queue);
                }
                context.SaveChanges();
            }
        }

        private static void ShuffleArray(string[] array)
        {
            Random random = new Random();
            for(int i = 0; i < array.Length; ++i)
            {
                string tmp = array[i];
                int r = random.Next(i, array.Length);
                array[i] = array[r];
                array[r] = tmp;
            }
        }
    }
}