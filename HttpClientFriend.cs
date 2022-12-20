using System.Text.Json;

namespace PrincessConsole
{
    public class HttpClientFriend : IFriend
    {
        private readonly string BaseAddress = "http://localhost:5000";
        private HttpClient _client = new HttpClient();
        private int _currentAttempt;

        public HttpClientFriend(AttemptNumber attempt)
        {
            _currentAttempt = attempt.Number;
            _client.BaseAddress = new Uri(BaseAddress);
        }

        public bool Compare(string aspirantName1, string aspirantName2)
        {
            var name = AsyncCompare(aspirantName1, aspirantName2).Result;
            return name.Equals(aspirantName1);
        }
        
        private async Task<string> AsyncCompare(string aspirantName1, string aspirantName2)
        {
            var postContent = new StringContent("{\n\t" + $"\"name1\": \"{aspirantName1}\",\t\"name2\": \"{aspirantName2}\"" + "\n}",
                System.Text.Encoding.Unicode, 
                "application/json");
            var response = await _client.PostAsync($"aspirants/friend/{_currentAttempt}/compare", postContent);
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