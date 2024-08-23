using ChatSignalR.DataAccess.AzureSQL.Models;
using Microsoft.AspNetCore.SignalR;
using ChatSignalR.Server.DTOs;
using System.Text.Json;
using ChatSignalR.Core.DTOs;
using ChatSignalR.Core.Services;
using ChatSignalR.Core.Interfaces.Services;

namespace ChatSignalR.Server.Hubs
{
    public interface IChatClient
    {
        public Task ReceiveMessage(MessageDto message);
    }

    public class ChatHub : Hub<IChatClient>
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessage(string chatName, string userName, string message)
        {
            var registrationResult = await _messageService.RegisterMessage(new MessageCreateDto(userName, chatName, message));
            await Clients.Group(chatName).ReceiveMessage(new MessageDto(userName, message, registrationResult.sentiment));
        }

        public async Task JoinChat(string chatName, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatName);
            await SendMessage(chatName, "Admin", $"{userName} just joined the chat!");
        }

        public async Task LeaveChat(string chatName, string userName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);
            await SendMessage(chatName, "Admin", $"{userName} just left the chat!");
        }
    }
}
