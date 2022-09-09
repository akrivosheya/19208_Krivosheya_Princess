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
            princess.ChooseGroom();
            Console.WriteLine("Groom is :" + princess.Groom);
            ResultWriter writer = new ResultWriter();
            writer.WriteResult(hall, princess.Groom);
        }
    }
}
