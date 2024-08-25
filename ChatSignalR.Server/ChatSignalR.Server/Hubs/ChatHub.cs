﻿using ChatSignalR.DataAccess.AzureSQL.Models;
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
        public Task RecieveMessagesList(IEnumerable<MessageDto> messsages);
    }

    public class ChatHub : Hub<IChatClient>
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IMessageService messageService, ILogger<ChatHub> logger)
        {
            _messageService = messageService;
            _logger = logger;

        }

        public async Task SendMessage(string chatName, string userName, string message)
        {
            try
            {
                var registrationResult = await _messageService.RegisterMessage(new MessageCreateDto(userName, chatName, message));
                Message? newMessage = await _messageService.Get(registrationResult);
                if(newMessage is null)
                    return;
                await Clients.Group(chatName).ReceiveMessage(new MessageDto(newMessage));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            
        }

        public async Task JoinChat(string chatName, string userName)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chatName);
                var messages = await _messageService.GetChatHistory(chatName);
                var messagesDtos = messages.Select(message => new MessageDto(message.AuthorName, message.MessageContent, message.Sentiment));
                await Clients.Client(Context.ConnectionId).RecieveMessagesList(messagesDtos);
                await SendMessage(chatName, "Admin", $"{userName} just joined the chat!");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            
        }

        public async Task LeaveChat(string chatName, string userName)
        {
            try
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);
                await SendMessage(chatName, "Admin", $"{userName} just left the chat!");
                Context.Abort();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
