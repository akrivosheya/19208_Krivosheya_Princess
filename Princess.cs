namespace PrincessConsole
{
    class Princess
    {
        public Princess(Servants servants, Friend friend)
        {
            this.servants = servants;
            this.friend = friend;
            wastedAspirants = new string[ASPIRANTS_COUNT];
            wastedAspirantsCount = (int)(ASPIRANTS_COUNT / Math.E);
        }
        public string chooseGroom()
        {
            int i = 0;
            for(; i < wastedAspirantsCount; ++i)
            {
                wastedAspirants[i] = servants.next();
            }
            string groom = null;
            for(; i < MAX_WASTED_ASPIRANT_COUNT; ++i)
            {
                string aspirant = servants.next();
                bool badAspirant = false;
                for(int j = 0; j < i; ++j)
                {
                    if(!friend.compare(aspirant, wastedAspirants[j]))
                    {
                        badAspirant = true;
                        break;
                    }
                }
                if(!badAspirant)
                {
                    groom = aspirant;
                    break;
                }
            }
            return groom;
        }

        private int ASPIRANTS_COUNT = 100;
        private int MAX_WASTED_ASPIRANT_COUNT = 50;

        private int wastedAspirantsCount;
        private string[] wastedAspirants;
        private Servants servants;
        private Friend friend;
    }
}
