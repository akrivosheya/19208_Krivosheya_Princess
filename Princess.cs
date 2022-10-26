using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PrincessConsole
{
    class Princess : IHostedService
    {

        private const int AspirantsCount = 100;
        private const int MaxWastedAspirantCount = 50;
        private const int WastedAspirantsCount = (int)(AspirantsCount / Math.E);
        private const string NoGroom = "";
        private const string MessageStoppedPrincess = "Princess was stoped";

        private IServiceScopeFactory _scopeFactory;
        private IServiceProvider _provider;
        private readonly ILogger<Princess> _logger;
        private string[] _wastedAspirants = new string[AspirantsCount];
        private string _groom;
        private bool _running = true;

        public Princess(IServiceProvider provider, IServiceScopeFactory scopeFactory)
        {
            _provider = provider;
            _scopeFactory = scopeFactory;
            _logger = provider.GetService<ILogger<Princess>>()!;
            _groom = NoGroom;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(ChooseGroom);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _running = false;
            return Task.CompletedTask;
        }

        public void ChooseGroom()
        {
            using(IServiceScope scope = _scopeFactory.CreateScope())
            {
                int i = 0;
                var friend = scope.ServiceProvider.GetService<Friend>()!;
                var hall = scope.ServiceProvider.GetService<Hall>()!;
                foreach(string aspirantName in hall)
                {
                    if(!_running)
                    {
                        _logger.LogInformation(MessageStoppedPrincess);
                        return;
                    }
                    if(i < WastedAspirantsCount)
                    {
                        _wastedAspirants[i] = aspirantName;
                    }
                    else
                    {
                        bool badAspirant = false;
                        for(int j = 0; j < i; ++j)
                        {
                            if(!friend.Compare(aspirantName, _wastedAspirants[j]))
                            {
                                badAspirant = true;
                                break;
                            }
                        }
                        if(!badAspirant)
                        {
                            _groom = aspirantName;
                            break;
                        }
                        else
                        {
                            _wastedAspirants[i] = aspirantName;
                        }
                    }
                    ++i;
                }
                _logger.LogInformation("Groom is : " + ((_groom.Equals(NoGroom)) ? "NO GROOM" : _groom));
                var writer = _provider.GetService<ResultWriter>()!;
                writer.WriteResult(hall, _groom);
            }
        }
    }
}
