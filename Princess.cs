namespace PrincessConsole
{
    class Princess
    {
        public Princess(Servants servants, Friend friend)
        {
            _servants = servants;
            _friend = friend;
            _wastedAspirants = new string[ASPIRANTS_COUNT];
            _wastedAspirantsCount = (int)(ASPIRANTS_COUNT / Math.E);
            Groom = NO_ASPIRANT;
        }

        public void chooseGroom()
        {
            int i = 0;
            for(; i < _wastedAspirantsCount; ++i)
            {
                _wastedAspirants[i] = _servants.next();
            }
            for(; i < MAX_WASTED_ASPIRANT_COUNT; ++i)
            {
                string aspirant = _servants.next();
                bool badAspirant = false;
                for(int j = 0; j < i; ++j)
                {
                    if(!_friend.compare(aspirant, _wastedAspirants[j]))
                    {
                        badAspirant = true;
                        break;
                    }
                }
                if(!badAspirant)
                {
                    Groom = aspirant;
                    break;
                }
                else
                {
                    _wastedAspirants[i] = aspirant;
                }
            }
        }

        public string Groom { get; private set; }

        private const int ASPIRANTS_COUNT = 100;
        private const int MAX_WASTED_ASPIRANT_COUNT = 50;
        private const string NO_ASPIRANT = "";

        private int _wastedAspirantsCount;
        private string[] _wastedAspirants;
        private Servants _servants;
        private Friend _friend;
    }
}
