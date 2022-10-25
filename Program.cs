namespace PrincessConsole
{
    class Program
    {
        static string NoGroom = "";
        static void Main(string[] args)
        {
            Hall hall = new Hall();
            Servants servants = new Servants(hall);
            Friend friend = new Friend(hall);
            Princess princess = new Princess(servants, friend);
            princess.ChooseGroom();
            string groom = princess.Groom;
            Console.WriteLine("Groom is : " + ((groom.Equals(NoGroom)) ? "NO GROOM" : groom));
            ResultWriter writer = new ResultWriter();
            writer.WriteResult(hall, princess.Groom);
        }
    }
}
