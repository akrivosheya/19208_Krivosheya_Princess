namespace PrincessConsole
{
    public class ControllerData
    {
        public readonly int AspirantsCount = 100;
        public readonly int NoAttempt = -1;
        public readonly int MinAttempt = 1;
        public readonly int MaxAttempt = 100;
        public readonly int FirstAspirant = 0;
        public Dictionary<string, Aspirant> Aspirants { get; } = new Dictionary<string, Aspirant>();
        public string[] Queue { get; }
        public int CurrentAttempt;
        public int CurrentAspirant;

        public ControllerData()
        {
            Queue = new string[AspirantsCount];
            CurrentAttempt = NoAttempt;
            CurrentAspirant = FirstAspirant;
        }
    }
}