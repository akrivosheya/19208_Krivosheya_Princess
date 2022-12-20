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
                return _currentAspirantIndex < AspirantsCount;
            }
        }
        private readonly string BaseAddress = "http://localhost:5000";
        private readonly string NoGroom = "";
        private readonly int FirstAspirant = 0;
        private readonly int DefaultQuality = 0;
        private HttpClient _client = new HttpClient();
        private HashSet<string> _wastedAspirants = new HashSet<string>();
        private int _groomQuality;
        private string _groomName;
        private int _currentAttempt;
        private int _currentAspirantIndex;
        private string _currentAspirantName = "";
        
        public HttpClientHall(AttemptNumber attempt)
        {
            _currentAttempt = attempt.Number;
            _currentAspirantIndex = FirstAspirant;
            _groomQuality = DefaultQuality;
            _groomName = NoGroom;
            _client.BaseAddress = new Uri(BaseAddress);
        }

        public string Next()
        {
            ++_currentAspirantIndex;
            var name = AsyncNext().Result;
            _currentAspirantName = name;
            _wastedAspirants.Add(name);
            return name;
        }

        public void Select()
        {
            _groomName = _currentAspirantName;
            _groomQuality = AsyncSelect().Result;
        }

        public IEnumerator<string> GetEnumerator()
        {
            AsyncReset().Wait();
            _currentAspirantIndex = FirstAspirant;
            for(int i = 0; i < AspirantsCount; ++i)
            {
                yield return AsyncNext().Result;
            }
        }

        public string this[int index]
        {
            get
            {
                return "";
            }
        }

        public Aspirant this[string name]
        {
            get
            {
                return new Aspirant(){ Name=name, 
                    Quality=(name.Equals(_groomName) ? _groomQuality : DefaultQuality), 
                    IsWasted=_wastedAspirants.Contains(name) };
            }
        }

        private async Task<string> AsyncNext()
        {
            var postContent = new StringContent("");
            var response = await _client.PostAsync($"aspirants/hall/{_currentAttempt}/next?session=1", postContent);
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

        private async Task AsyncReset()
        {
            var postContent = new StringContent("");
            var response = await _client.PostAsync($"hall/reset", postContent);
            var stringBody = await response.Content.ReadAsStringAsync();
            return;
        }

        private async Task<int> AsyncSelect()
        {
            var postContent = new StringContent("");
            var response = await _client.PostAsync($"aspirants/hall/{_currentAttempt}/select", postContent);
            var stringBody = await response.Content.ReadAsStringAsync();
            var body = JsonSerializer.Deserialize<QualityDto>(stringBody);
            var rank = body!.rank;
            if(rank == null)
            {
                throw new NoAspirantException($"There is no more aspirants");
            }
            else
            {
                return (int)rank;
            }
        }
    }
}