using System.Threading;
using System.Threading.Tasks;
using Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace Services
{
    public class CallBoardService : BackgroundService
    {
        public CallBoardService(IHubContext<CallBoardHub> hub)
        {
            _hub = hub;
        }

        private readonly IHubContext<CallBoardHub> _hub;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _hub.Clients.All.SendAsync("CallBoardService");
                await Task.Delay(1000);
            }
        }
    }
}