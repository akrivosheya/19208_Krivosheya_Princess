namespace PrincessConsole
{
    public class StringListResultWriter : IResultWriter
    {
        public List<string> StringList { get => _stringList; }
        private List<string> _stringList = new List<string>();

        private const string FileName = "princessHappiness.txt";
        private const string NoGroom = "";
        private const string UnknownAspirant = "Unknown Aspirant";
        private const int MinQuality = 50;
        private const int LonelinessHappinessPoints = 10;
        private const int UnhappinessPoints = 0;

        public void WriteResult(Hall hall, string groom)
        {
            _stringList.Clear();
            foreach(string aspirantName in hall)
            {
                _stringList.Add($"Aspirant: {aspirantName}, Quality: {hall[aspirantName]?.Quality}");
            }
            if(groom == NoGroom)
            {
                _stringList.Add(LonelinessHappinessPoints.ToString());
            }
            else
            {
                int? groomQuality = hall[groom]?.Quality;
                if(groomQuality == null){
                    _stringList.Add(UnknownAspirant);
                }
                else if(groomQuality <= MinQuality)
                {
                    _stringList.Add(groom + " with " + groomQuality + " => " + UnhappinessPoints);
                }
                else
                {
                    _stringList.Add(groom + " with " + groomQuality);
                }
            }
        }
    }
}