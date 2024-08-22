using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace ChatSignalR.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string userName, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", userName, message);
        }
    }
}
