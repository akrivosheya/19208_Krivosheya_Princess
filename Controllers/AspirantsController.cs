using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PrincessConsole
{
    [ApiController]
    [Route("aspirants")]
    public class AspirantsController : ControllerBase
    {
        public readonly int AspirantsCount = 100;
        private readonly int NoAttempt = -1;
        private readonly int MinAttempt = 1;
        private readonly int MaxAttempt = 100;
        private readonly int FirstAspirant = 0;
        private readonly ILogger<AspirantsController> _logger;
        private AttemptContenderGenerator _generator;
        private Dictionary<string, Aspirant> _aspirants = new Dictionary<string, Aspirant>();
        private string[] _queue;
        private int _currentAttempt;
        private int _currentAspirant;

        public AspirantsController(ILogger<AspirantsController> logger, AttemptContenderGenerator generator)
        {
            _queue = new string[AspirantsCount];
            _currentAttempt = NoAttempt;
            _currentAspirant = FirstAspirant;
            _logger = logger;
            _generator = generator;
        }

        [HttpPost("hall/{attempt}/next")]
        public string Next(int attempt, int session)
        {
            if(attempt != _currentAttempt)
            {
                if(attempt <= MinAttempt || attempt >= MaxAttempt)
                {
                    throw new NoSuchAttempt($"Wrong attemtp: attempt has to be integer between {MinAttempt} and {MaxAttempt}");
                }
                _generator.Attempt = attempt;
                _generator.GenerateContenders(_queue, _aspirants);
                _currentAspirant = FirstAspirant;
            }
            _currentAspirant = (_currentAspirant == AspirantsCount) ? _currentAspirant : _currentAspirant + 1;
            return "{\n\tname: " + this[_currentAspirant - 1] + ",\n}";
        }

        [HttpPost("hall/{attempt}/select")]
        public string Select(int attempt, int session)
        {
            if(attempt != _currentAttempt)
            {
                if(attempt <= MinAttempt || attempt >= MaxAttempt)
                {
                    throw new NoSuchAttempt($"Wrong attemtp: attempt has to be integer between {MinAttempt} and {MaxAttempt}");
                }
                _generator.Attempt = attempt;
                _generator.GenerateContenders(_queue, _aspirants);
                _currentAspirant = FirstAspirant;
            }
            _currentAspirant = (_currentAspirant == AspirantsCount) ? _currentAspirant : _currentAspirant + 1;
            int rank = _aspirants[_queue[_currentAspirant - 1]].Quality;
            return "{\n\trank: " + rank + ",\n}";
        }

        [HttpPost("friend/{attempt}/compare")]
        public string Compare(int attempt, int session, [FromBody] string stringBody)
        {
            var body = JsonSerializer.Deserialize<ComparingAspirantsDto>(stringBody);
            if(!_aspirants.ContainsKey(body!.name1))
            {
                throw new StrangerAspirantException($"In current attempt there is not aspirant wit name {body.name1}");
            }
            if(!_aspirants.ContainsKey(body!.name2))
            {
                throw new StrangerAspirantException($"In current attempt there is not aspirant wit name {body.name2}");
            }
            Aspirant aspirant1 = _aspirants[body!.name1];
            Aspirant aspirant2 = _aspirants[body!.name2];
            if(!aspirant1.IsWasted && !aspirant2.IsWasted)
            {
                throw new StrangerAspirantException($"Friend can't compare aspirants: princess didn't meet {body.name1} and {body.name2}");
            }
            var nameOfBestAspirant = (aspirant1 > aspirant2) ? body.name1 : body.name2;
            return "{\n\tname: " + nameOfBestAspirant + ",\n}";
        }

        [HttpGet("get/{lol}")]
        public string Get(int lol, int shit)
        {
            return "get" + lol + shit;
        }

        public string this[int index]
        {
            get
            {
                if(index >= AspirantsCount || index < 0)
                {
                    return "null";
                }
                return "\"" + _queue[index] + "\"";
            }
        }
    }
}