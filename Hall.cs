using System.Text;

namespace PrincessConsole
{
    class Hall
    {
        public readonly int AspirantsCount = 100;
        private const int MaxNameLength = 10;
        private const int MinNameLength = 1;
        private const string NoAspirant = "";

        private Dictionary<string, Aspirant> _aspirants = new Dictionary<string, Aspirant>();
        private string[] _queue;
        
        public Hall()
        {
            _queue = new string[AspirantsCount];
            for(int i = 0; i < AspirantsCount; ++i)
            {
                string randomName = GetRandomName();
                while(_aspirants.ContainsKey(randomName)){
                    randomName = GetRandomName();
                }
                _aspirants.Add(randomName, new Aspirant { Name=randomName, Quality=(i + 1) });
                _queue[i] = randomName;
            }
            ShuffleArray(_queue);
        }

        public string this[int index]
        {
            get
            {
                if(index > AspirantsCount)
                {
                    return NoAspirant;
                }
                return _queue[index];
            }
        }

        public Aspirant? this[string name]
        {
            get
            {
                return _aspirants[name] as Aspirant;
            }
        }

        private string GetRandomName()
        {
            Random random = new Random();
            int nameLength = random.Next(MinNameLength, MaxNameLength);
            StringBuilder name = new StringBuilder(nameLength);
            for(int i = 0; i < nameLength; ++i)
            {
                char nextSymb = (char)random.Next((int)'a', (int)'z');
                name.Append(nextSymb);
            }
            return name.ToString();
        }

        private void ShuffleArray(string[] array)
        {
            Random random = new Random();
            for(int i = 0; i < array.Length; ++i)
            {
                string tmp = array[i];
                int r = random.Next(i, array.Length);
                array[i] = array[r];
                array[r] = tmp;
            }
        }
    }
}