namespace PrincessConsole
{
    public class ContenderGeneratorWithMaxInCenter : IContenderGenerator
    {
        public void GenerateContenders(string[] queue, Dictionary<string, Aspirant> aspirants)
        {
            for(int i = 0; i < queue.Length; ++i)
            {
                if(i == 50)
                {
                    aspirants.Add(i.ToString(), new Aspirant { Name=i.ToString(), Quality=(100), IsWasted=false });
                }
                else
                {
                    aspirants.Add(i.ToString(), new Aspirant { Name=i.ToString(), Quality=(1), IsWasted=false });
                }
                queue[i] = i.ToString();
            }
        }
    }
}