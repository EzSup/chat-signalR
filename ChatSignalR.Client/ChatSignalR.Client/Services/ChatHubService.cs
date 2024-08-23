using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatSignalR.Client.Services
{
    public class ChatHubService : IAsyncDisposable
    {
        private readonly HubConnection _hubConnection;

        public ChatHubService()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7051/chat")
                .WithAutomaticReconnect()
                .Build();
            
        }

        public HubConnection HubConnection { get { return _hubConnection; } private set { } }

        public async Task StartAsync()
        {
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        public async Task StopAsync()
        {
            await _hubConnection.StopAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
            }
        }
    }
}
