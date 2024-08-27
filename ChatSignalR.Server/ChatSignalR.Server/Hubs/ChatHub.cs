using ChatSignalR.DataAccess.AzureSQL.Models;
using Microsoft.AspNetCore.SignalR;
using ChatSignalR.Server.DTOs;
using System.Text.Json;
using ChatSignalR.Core.DTOs;
using ChatSignalR.Core.Services;
using ChatSignalR.Core.Interfaces.Services;

namespace ChatSignalR.Server.Hubs
{
    /// <summary>
    /// Defines the methods that a chat client must implement to handle incoming messages and lists of messages.
    /// </summary>
    public interface IChatClient
    {
        /// <summary>
        /// Receives a single message from the chat server.
        /// </summary>
        /// <param name="message">The message to be received.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task ReceiveMessage(MessageDto message);

        /// <summary>
        /// Receives a list of messages from the chat server.
        /// </summary>
        /// <param name="messages">A collection of messages to be received.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task ReceiveMessagesList(IEnumerable<MessageDto> messages);
    }

    /// <summary>
    /// Represents a SignalR hub that handles chat-related functionality, including sending and receiving messages, and managing user participation in chats.
    /// </summary>
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IMessageService messageService, ILogger<ChatHub> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        /// <summary>
        /// Sends a message to a specified chat group.
        /// </summary>
        /// <param name="chatName">The name of the chat group.</param>
        /// <param name="userName">The name of the user sending the message.</param>
        /// <param name="message">The content of the message.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task SendMessage(string chatName, string userName, string message)
        {
            try
            {
                var messageId = await _messageService.RegisterMessage(new MessageCreateDto(userName, chatName, message));
                Message? newMessage = await _messageService.Get(messageId);
                if(newMessage is null)
                    return;
                await Clients.Group(chatName).ReceiveMessage(new MessageDto(newMessage));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }            
        }

        /// <summary>
        /// Adds a user to a chat group and sends the chat history to the user.
        /// </summary>
        /// <param name="chatName">The name of the chat group to join.</param>
        /// <param name="userName">The name of the user joining the chat.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task JoinChat(string chatName, string userName)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chatName);
                var messages = await _messageService.GetChatHistory(chatName);
                var messagesDtos = messages.Select(message => new MessageDto(message.AuthorName, message.MessageContent, message.Sentiment, message.Created));
                await Clients.Client(Context.ConnectionId).ReceiveMessagesList(messagesDtos);
                await SendMessage(chatName, "Admin", $"{userName} just joined the chat!");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }            
        }

        /// <summary>
        /// Removes a user from a chat group and notifies the group.
        /// </summary>
        /// <param name="chatName">The name of the chat group to leave.</param>
        /// <param name="userName">The name of the user leaving the chat.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task LeaveChat(string chatName, string userName)
        {
            try
            {
                await SendMessage(chatName, "Admin", $"{userName} just left the chat!");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
