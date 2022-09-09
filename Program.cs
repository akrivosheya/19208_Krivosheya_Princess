namespace PrincessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Hall hall = new Hall();
            Servants servants = new Servants(hall);
            Friend friend = new Friend(hall);
            Princess princess = new Princess(servants, friend);
            Console.WriteLine(princess.chooseGroom());
        }
    }
}
