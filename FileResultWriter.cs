namespace PrincessConsole
{
    public class FileResultWriter : IResultWriter
    {
        private const string FileName = "princessHappiness.txt";
        private const string NoGroom = "";
        private const string UnknownAspirant = "Unknown Aspirant";
        private const int MinQuality = 50;
        private const int LonelinessHappinessPoints = 10;
        private const int UnhappinessPoints = 0;

        public void WriteResult(IHall hall, string groom)
        {
            using(StreamWriter stream = File.CreateText(FileName))
            {
                foreach(string aspirantName in hall)
                {
                    stream.WriteLine($"Aspirant: {aspirantName}, Quality: {hall[aspirantName]?.Quality}");
                }
                if(groom == NoGroom)
                {
                    stream.WriteLine(LonelinessHappinessPoints);
                }
                else
                {
                    int? groomQuality = hall[groom]?.Quality;
                    if(groomQuality == null)
                    {
                        stream.WriteLine(UnknownAspirant);
                    }
                    else if(groomQuality <= MinQuality)
                    {
                        stream.WriteLine(groom + " with " + groomQuality + " => " + UnhappinessPoints);
                    }
                    else
                    {
                        stream.WriteLine(groom + " with " + groomQuality);
                    }
                }
            }
        }
    }
}