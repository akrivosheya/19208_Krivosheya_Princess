using System.Text.Json;

namespace PrincessConsole
{
    public class HttpClientHall : IHall
    {
        public int AspirantsCount { get; } = 100;
        public bool HasNext
        {
            get
            {
                return _currentAspirant < _queue.Length;
            }
        }
        private readonly string BaseAddress = "https://localhost:5000/aspirants";
        private HttpClient _client = new HttpClient();
        private int _currentAttempt;
        
        public HttpClientHall(AttemptNumber attempt)
        {
            _currentAttempt = attempt.Number;
            _client.BaseAddress = new Uri(BaseAddress);
        }

        public string Next()
        {
            return AsyncNext();
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

        private async Task<string> AsyncNext()
        {
            var postContent = new StringContent("");
            var response = await _client.PostAsync($"hall/{_currentAttempt}/next", postContent);
            var stringBody = await response.Content.ReadAsStringAsync();
            var body = JsonSerializer.Deserialize<NameDto>(stringBody);
            var name = body!.name;
            if(name == null)
            {
                throw new NoAspirantException($"There is no more aspirants");
            }
            else
            {
                return name;
            }
        }
    }
}