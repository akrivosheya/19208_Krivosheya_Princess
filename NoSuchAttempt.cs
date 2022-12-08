namespace PrincessConsole
{
    public class NoSuchAttempt : Exception
    {
        public NoSuchAttempt(string message) : base (message)
        {
        }
    }
}