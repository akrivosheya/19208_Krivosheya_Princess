namespace PrincessConsole
{
    public class Friend : IFriend
    {
        private Hall _hall;

        public Friend(Hall hall)
        {
            _hall = hall;
        }

        public bool Compare(string aspirantName1, string aspirantName2)
        {
            Aspirant aspirant1 = _hall[aspirantName1];
            Aspirant aspirant2 = _hall[aspirantName2];
            if(!aspirant1.IsWasted && !aspirant2.IsWasted)
            {
                throw new StrangerAspirantException($"Friend can't compare aspirants: princess didn't meet {aspirantName1} and {aspirantName2}");
            }
            return aspirant1 > aspirant2;
        }
    }
}