namespace PrincessConsole
{
    class Princess
    {
        public string Groom { get; private set; }

        private const int AspirantsCount = 100;
        private const int MaxWastedAspirantCount = 50;
        private const string NoAspirant = "";

        private int _wastedAspirantsCount = (int)(AspirantsCount / Math.E);
        private string[] _wastedAspirants = new string[AspirantsCount];
        private Servants _servants;
        private Friend _friend;

        public Princess(Servants servants, Friend friend)
        {
            _servants = servants;
            _friend = friend;
            Groom = NoAspirant;
        }

        public void ChooseGroom()
        {
            int i = 0;
            for(; i < _wastedAspirantsCount; ++i)
            {
                _wastedAspirants[i] = _servants.Next()!;
            }
            for(; i < MaxWastedAspirantCount; ++i)
            {
                string? aspirant = _servants.Next();
                if(aspirant == null)
                {
                    break;
                }
                bool badAspirant = false;
                for(int j = 0; j < i; ++j)
                {
                    if(!_friend.Compare(aspirant!, _wastedAspirants[j]))
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
    }
}
