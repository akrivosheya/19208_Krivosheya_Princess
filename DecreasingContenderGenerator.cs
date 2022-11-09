namespace PrincessConsole
{
    public class DecreasingContenderGenerator : IContenderGenerator
    {
        public void GenerateContenders(string[] queue, Dictionary<string, Aspirant> aspirants)
        {
            for(int i = 0; i < queue.Length; ++i)
            {
                aspirants.Add(i.ToString(), new Aspirant { Name=i.ToString(), Quality=(queue.Length - i), IsWasted=false });
                queue[i] = i.ToString();
            }
        }
    }
}