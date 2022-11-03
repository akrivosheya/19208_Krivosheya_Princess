using System.Text;

namespace PrincessConsole
{
    public class RandomContenderGenerator : IContenderGenerator
    {
        private const int MaxNameLength = 10;
        private const int MinNameLength = 1;

        public void GenerateContenders(string[] queue, Dictionary<string, Aspirant> aspirants)
        {
            for(int i = 0; i < queue.Length; ++i)
            {
                string randomName = GetRandomName();
                while(aspirants.ContainsKey(randomName)){
                    randomName = GetRandomName();
                }
                aspirants.Add(randomName, new Aspirant { Name=randomName, Quality=(i + 1), IsWasted=false });
                queue[i] = randomName;
            }
            ShuffleArray(queue);
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