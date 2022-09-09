using System.Collections;
using System.Text;

namespace PrincessConsole
{
    class Hall
    {
        public Hall()
        {
            queue = new string[ASPIRANTS_COUNT];
            for(int i = 0; i < ASPIRANTS_COUNT; ++i)
            {
                string randomName = getRandomName();
                while(aspirants.ContainsKey(randomName)){
                    randomName = getRandomName();
                }
                aspirants.Add(randomName, new Aspirant(randomName, i));
                queue[i] = randomName;
            }
            shuffleArray(queue);
        }

        public string next(int index)
        {
            if(index > ASPIRANTS_COUNT)
            {
                return "";
            }
            return queue[index];
        }

        public object getAspirant(string name)
        {
            return aspirants[name] ?? new Aspirant("badAspirant", -1);// нормально передать
        }

        private string getRandomName()
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

        private void shuffleArray(string[] array)
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

        private int ASPIRANTS_COUNT = 100;
        private int MAX_NAME_LENGTH = 10;
        private int MIN_NAME_LENGTH = 1;

        private Hashtable aspirants = new Hashtable();
        private string[] queue;
    }
}