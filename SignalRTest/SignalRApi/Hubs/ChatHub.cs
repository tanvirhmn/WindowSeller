using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.Hubs
{
    public sealed class ChatHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("RecieveMessage", $"{Context.ConnectionId} has joined"); 
        }
    }
}
