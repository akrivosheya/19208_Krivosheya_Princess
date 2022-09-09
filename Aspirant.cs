namespace PrincessConsole
{
    class Aspirant
    {
        public Aspirant(string name, int quality)
        {
            this.name = name;
            this.quality = quality;
        }

        public string getName()
        {
            return name;
        }

        public int getQuality()
        {
            return quality;
        }

        public bool getIsWasted()
        {
            return isWasted;
        }

        public void setIsWasted(bool isWasted)
        {
            this.isWasted = isWasted;
        }

        private string name;
        private int quality;
        private bool isWasted;
    }
}