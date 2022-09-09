namespace PrincessConsole
{
    class Servants
    {
        public Servants(Hall hall)
        {
            this.hall = hall;
        }

        public string next()
        {
            return hall.next(nextAspirant);
        }

        private int nextAspirant = 0;
        private Hall hall;
    }
}