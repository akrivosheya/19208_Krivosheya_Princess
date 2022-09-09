using System.IO;

namespace PrincessConsole
{
    class ResultWriter
    {
        private const string FILE_NAME = "princessHappiness.txt";
        private const int MIN_QUALITY = 50;
        private const int LONELINESS_HAPPINESS_POINTS = 10;
        private const int UNHAPPINESS_POINTS = 0;

        public void WriteResult(Hall hall, string groom)
        {
            using(StreamWriter stream = File.CreateText(FILE_NAME))
            {
                for(int i = 0; i < hall.ASPIRANTS_COUNT; ++i)
                {
                    stream.WriteLine(hall[i]);
                }
                if(groom == "")
                {
                    stream.WriteLine(LONELINESS_HAPPINESS_POINTS);
                }
                else
                {
                    int groomQuality = ((Aspirant)hall[groom]).Quality;
                    if(groomQuality <= MIN_QUALITY)
                    {
                        stream.WriteLine(UNHAPPINESS_POINTS);
                    }
                    else
                    {
                        stream.WriteLine(groomQuality);
                    }
                }
            }
        }
    }
}