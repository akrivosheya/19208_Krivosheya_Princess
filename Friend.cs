namespace PrincessConsole
{
    class Friend
    {
        private Hall _hall;

        public Friend(Hall hall)
        {
            _hall = hall;
        }

        public bool Compare(string aspirantName1, string aspirantName2)
        {
            Aspirant? aspirant1 = _hall[aspirantName1] as Aspirant;
            Aspirant? aspirant2 = _hall[aspirantName2] as Aspirant;
            if(aspirant2 == null)
            {
                return true;
            }
            if(aspirant1 == null)
            {
                return false;
            }
            return aspirant1 > aspirant2;
        }
    }
}