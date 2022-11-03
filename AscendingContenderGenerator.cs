namespace PrincessConsole
{
    public class AscendingContenderGenerator : IContenderGenerator
    {
        public void GenerateContenders(string[] queue, Dictionary<string, Aspirant> aspirants)
        {
            for(int i = 0; i < queue.Length; ++i)
            {
                aspirants.Add(i.ToString(), new Aspirant { Name=i.ToString(), Quality=(i + 1), IsWasted=false });
                queue[i] = i.ToString();
            }
        }
    }
}