using ChatSignalR.Client.Models;

namespace ChatSignalR.Client.Services
{
    public interface IChatHubService : IAsyncDisposable
    {
        event Func<Exception, Task>? OnDisconnected;
        event Action? OnConnected;
        event Action<IEnumerable<ChatMessage>>? OnMessageListReceived;
        event Action<ChatMessage>? OnMessageReceived;

        Task Сonnect();
        Task JoinChat(string chatName, string userName);
        Task SendMessage(string chatName, string userName, string message);
        Task LeaveChat(string chatName, string userName);

        bool IsConnected();
        bool IsConnecting();

        
    }
}
