namespace PrincessConsole
{
    class Friend
    {
        public Friend(Hall hall)
        {
            _hall = hall;
        }

        public bool compare(string aspirantName1, string aspirantName2)
        {
            Aspirant aspirant1 = (Aspirant)_hall.getAspirant(aspirantName1);
            Aspirant aspirant2 = (Aspirant)_hall.getAspirant(aspirantName2);
            return aspirant1.Quality > aspirant2.Quality;
        }

        private Hall _hall;
    }
}