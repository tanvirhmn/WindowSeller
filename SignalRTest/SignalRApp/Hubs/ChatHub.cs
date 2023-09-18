﻿using Microsoft.AspNetCore.SignalR;

namespace SignalRApp.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("RecieveMessage",user, message);
        }
    }
}
