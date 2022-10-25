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

        public string? Next()
        {
            string aspirantName = _hall[_nextAspirant++];
            return (aspirantName.Equals("")) ? aspirantName : null;
        }
    }
}