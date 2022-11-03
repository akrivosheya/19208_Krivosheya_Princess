namespace PrincessConsole
{
    public interface IContenderGenerator
    {
        void GenerateContenders(string[] queue, Dictionary<string, Aspirant> aspirants);
    }
}