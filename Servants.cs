namespace PrincessConsole
{
    class Servants
    {
        public Servants(Hall hall)
        {
            _hall = hall;
        }

        public string next()
        {
            return _hall.next(_nextAspirant++);
        }

        private int _nextAspirant = 0;
        private Hall _hall;
    }
}