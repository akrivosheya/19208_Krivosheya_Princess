namespace PrincessConsole
{
    public interface IHall
    {
        public int AspirantsCount { get; }
        public bool HasNext { get; }

        public string Next();
        public IEnumerator<string> GetEnumerator();
        public string this[int index] { get; }
        public Aspirant this[string name] { get; }
    }
}