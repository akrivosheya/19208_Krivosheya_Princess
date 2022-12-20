using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PrincessConsole
{
    [ApiController]
    [Route("aspirants")]
    public class AspirantsController : ControllerBase
    {
        private readonly ILogger<AspirantsController> _logger;
        private IServiceProvider _provider;
        private ControllerData _data;

        public AspirantsController(ILogger<AspirantsController> logger, IServiceProvider provider, ControllerData data)
        {
            _logger = logger;
            _provider = provider;
            _data = data;
        }

        [HttpPost("hall/reset")]
        public string Reset(int session)
        {
            _data.CurrentAspirant = _data.FirstAspirant;
            return "{\n}";
        }

        [HttpPost("hall/{attempt}/next")]
        public string Next(int attempt, int session)
        {
            if(attempt != _data.CurrentAttempt)
            {
                if(attempt < _data.MinAttempt || attempt > _data.MaxAttempt)
                {
                    throw new NoSuchAttempt($"Wrong attemtp: attempt has to be integer between {_data.MinAttempt} and {_data.MaxAttempt}");
                }
                var generator = _provider.GetService<IContenderGenerator>() as AttemptContenderGenerator;
                generator!.Attempt = attempt;
                generator.GenerateContenders(_data.Queue, _data.Aspirants);
                _data.CurrentAspirant = _data.FirstAspirant;
                _data.CurrentAttempt = attempt;
            }
            _data.CurrentAspirant = (_data.CurrentAspirant == _data.AspirantsCount) ? _data.CurrentAspirant : _data.CurrentAspirant + 1;
            _data.Aspirants[_data.Queue[_data.CurrentAspirant - 1]].IsWasted = true;
            return "{\n\t\"name\": " + this[_data.CurrentAspirant - 1] + "\n}";
        }

        [HttpPost("hall/{attempt}/select")]
        public string Select(int attempt, int session)
        {
            if(attempt != _data.CurrentAttempt)
            {
                if(attempt < _data.MinAttempt || attempt > _data.MaxAttempt)
                {
                    throw new NoSuchAttempt($"Wrong attemtp: attempt has to be integer between {_data.MinAttempt} and {_data.MaxAttempt}");
                }
                var generator = _provider.GetService<AttemptContenderGenerator>()!;
                generator.Attempt = attempt;
                generator.GenerateContenders(_data.Queue, _data.Aspirants);
                _data.CurrentAspirant = _data.FirstAspirant;
                _data.CurrentAttempt = attempt;
            }
            int rank = _data.Aspirants[_data.Queue[_data.CurrentAspirant - 1]].Quality;
            return "{\n\t\"rank\": " + rank + "\n}";
        }

        [HttpPost("friend/{attempt}/compare")]
        public string Compare(int attempt, int session, [FromBody] ComparingAspirantsDto body)
        {
            if(!_data.Aspirants.ContainsKey(body!.name1))
            {
                throw new StrangerAspirantException($"In current attempt there is not aspirant wit name {body.name1}");
            }
            if(!_data.Aspirants.ContainsKey(body!.name2))
            {
                throw new StrangerAspirantException($"In current attempt there is not aspirant wit name {body.name2}");
            }
            Aspirant aspirant1 = _data.Aspirants[body!.name1];
            Aspirant aspirant2 = _data.Aspirants[body!.name2];
            if(!aspirant1.IsWasted && !aspirant2.IsWasted)
            {
                throw new StrangerAspirantException($"Friend can't compare aspirants: princess didn't meet {body.name1} and {body.name2}");
            }
            var nameOfBestAspirant = (aspirant1 > aspirant2) ? body.name1 : body.name2;
            return "{\n\t\"name\": \"" + nameOfBestAspirant + "\"\n}";
        }

        [HttpGet("get")]
        public string Get()
        {
            return "get";
        }

        public string this[int index]
        {
            get
            {
                if(index >= _data.AspirantsCount || index < 0)
                {
                    return "null";
                }
                return "\"" + _data.Queue[index] + "\"";
            }
        }
    }
}