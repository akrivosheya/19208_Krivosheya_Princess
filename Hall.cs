namespace PrincessConsole
{
    public class Hall
    {
        public readonly int AspirantsCount = 100;

        private Dictionary<string, Aspirant> _aspirants = new Dictionary<string, Aspirant>();
        private string[] _queue;
        
        public Hall(ContenderGenerator generator)
        {
            _queue = new string[AspirantsCount];
            generator.GenerateContenders(_queue, _aspirants);
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach(string aspirantName in _queue)
            {
                yield return aspirantName;
            }
        }

        public string this[int index]
        {
            get
            {
                if(index > AspirantsCount)
                {
                    throw new NoAspirantException($"There is no aspirant with index {index}");
                }
                return _queue[index];
            }
        }

        public Aspirant this[string name]
        {
            get
            {
                if(!_aspirants.ContainsKey(name))
                {
                    throw new NoAspirantException($"There is no aspirant with name {name}");
                }
                return (_aspirants[name] as Aspirant)!;
            }
        }
    }
}