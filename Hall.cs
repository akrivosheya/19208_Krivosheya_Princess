using System.Collections;
using System.Text;

namespace PrincessConsole
{
    class Hall
    {
        public readonly int ASPIRANTS_COUNT = 100;
        private int MAX_NAME_LENGTH = 10;
        private int MIN_NAME_LENGTH = 1;

        private Hashtable aspirants = new Hashtable();
        private string[] queue;
        
        public Hall()
        {
            queue = new string[ASPIRANTS_COUNT];
            for(int i = 0; i < ASPIRANTS_COUNT; ++i)
            {
                string randomName = GetRandomName();
                while(aspirants.ContainsKey(randomName)){
                    randomName = GetRandomName();
                }
                aspirants.Add(randomName, new Aspirant(randomName, i + 1));
                queue[i] = randomName;
            }
            ShuffleArray(queue);
        }

        public string this[int index]
        {
            get
            {
                if(index > ASPIRANTS_COUNT)
                {
                    return "";
                }
                return queue[index];
            }
        }

        public Aspirant this[string name]
        {
            get
            {
                return aspirants[name] as Aspirant ?? new Aspirant("", -1);
            }
        }

        private string GetRandomName()
        {
            Random random = new Random();
            int nameLength = random.Next(MIN_NAME_LENGTH, MAX_NAME_LENGTH);
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
            if(array is null)
            {
                return;
            }
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