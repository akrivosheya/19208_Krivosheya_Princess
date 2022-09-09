namespace PrincessConsole
{
    class Aspirant
    {
        public Aspirant(string name, int quality)
        {
            Name = name;
            Quality = quality;
        }

        public string Name { get; private set; }

        public int Quality { get; private set; }

        public bool IsWasted { get; set; }
    }
}