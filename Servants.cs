namespace PrincessConsole
{
    class Servants
    {
        private int _nextAspirant = 0;
        private Hall _hall;

        public Servants(Hall hall)
        {
            _hall = hall;
        }

        public string Next()
        {
            return _hall[_nextAspirant++];
        }
    }
}