using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Services;

namespace Hubs
{
    public class CallBoardHub : Hub
    {
        public CallBoardHub(CallsService callsService)
        {
            _callsService = callsService;
        }
        private readonly CallsService _callsService;
        public async Task GetCallsList(string login) => await Clients.Caller.SendAsync("CallsList", _callsService.GetCallsList(login));        
    }
}