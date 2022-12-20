namespace PrincessConsole
{
    public class Hall : IHall
    {
        public int AspirantsCount { get; } = 100;
        public bool HasNext
        {
            get
            {
                return _currentAspirant < _queue.Length;
            }
        }

        private Dictionary<string, Aspirant> _aspirants = new Dictionary<string, Aspirant>();
        private string[] _queue;
        private int _currentAspirant = 0;
        
        public Hall(IContenderGenerator generator)
        {
            _queue = new string[AspirantsCount];
            generator.GenerateContenders(_queue, _aspirants);
        }

        public string Next()
        {
            return this[_currentAspirant++];
        }

        public void Select()
        {
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
                if(index >= AspirantsCount || index < 0)
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
                    throw new StrangerAspirantException($"There is no aspirant with name {name}");
                }
                return (_aspirants[name] as Aspirant)!;
            }
        }
    }
}