namespace PrincessConsole
{
    class Friend
    {
        public Friend(Hall hall)
        {
            this.hall = hall;
        }

        public bool compare(string aspirantName1, string aspirantName2)
        {
            Aspirant aspirant1 = hall.getAspirant(aspirantName1);
            Aspirant aspirant2 = hall.getAspirant(aspirantName2);
            return aspirant1.getQuality() > aspirant2.getQuality();
        }

        private Hall hall;
    }
}