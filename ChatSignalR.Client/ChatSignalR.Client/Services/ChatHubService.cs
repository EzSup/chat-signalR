using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using static ChatSignalR.Client.Components.Pages.Chat;

namespace ChatSignalR.Client.Services
{
    public class ChatHubService : IAsyncDisposable
    {
        private readonly HubConnection _hubConnection;

        public ChatHubService(string link)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(link)
                .WithAutomaticReconnect()
                .Build();


            _hubConnection.On<IEnumerable<ChatMessage>>("ReceiveMessagesList", (messages) =>
            {
                if (OnMessageListReceived != null)
                {
                    OnMessageListReceived.Invoke(messages);
                }
            });

            _hubConnection.On<ChatMessage>("ReceiveMessage", (message) =>
            {
                if (OnMessageReceived != null)
                {
                    OnMessageReceived.Invoke(message);
                }
            });

        }

        //public HubConnection HubConnection { get { return _hubConnection; } private set { } }

        public async Task JoinChat(string chatName, string userName)
        {
            await _hubConnection.InvokeAsync("JoinChat", chatName, userName);
        }

        public async Task SendMessage(string chatName, string userName, string message)
        {            
            await _hubConnection.SendAsync("SendMessage", chatName, userName, message);
        }

        public async Task LeaveChat(string chatName, string userName)
        {
            if (IsConnected())
            {
                await _hubConnection.InvokeAsync("JoinChat", chatName, userName);
            }
        }

        //events for future snackbar info messages showing
        public event Func<Task> OnConnectedAsync;
        public event Action OnConnected;
        public event Action<IEnumerable<ChatMessage>> OnMessageListReceived;
        public event Action<ChatMessage> OnMessageReceived;

        public bool IsConnected()
        {
            return _hubConnection.State == HubConnectionState.Connected;
        }

        public bool IsConnecting()
        {
            return _hubConnection.State == HubConnectionState.Connecting;
        }

        public async Task Сonnect()
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                await _hubConnection.StartAsync();
                _hubConnection.Closed += Retry;
            }
            if (IsConnected() && OnConnectedAsync != null && OnConnected != null)
            {
                await OnConnectedAsync.Invoke();
                OnConnected.Invoke();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
            }
        }

        private async Task Retry(Exception ex)
        {
            Console.WriteLine($"Connection closed due to error: {ex}");
            await Task.Delay(1000);
            await _hubConnection.StartAsync();
        }
    }
}
