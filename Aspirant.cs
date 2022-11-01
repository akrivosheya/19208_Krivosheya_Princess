namespace PrincessConsole
{
    public class Aspirant
    {
        public string Name { get; init; } = "";
        public int Quality { get; init; }
        public bool IsWasted { get; set; } = false;

        public static bool operator <(Aspirant aspirant1, Aspirant aspirant2)
        {
            return aspirant1.Quality < aspirant2.Quality;
        }

        public static bool operator >(Aspirant aspirant1, Aspirant aspirant2)
        {
            return aspirant1.Quality > aspirant2.Quality;
        }
    }
}