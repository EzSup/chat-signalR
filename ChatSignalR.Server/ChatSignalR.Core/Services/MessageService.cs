using ChatSignalR.Core.DTOs;
using ChatSignalR.Core.Interfaces.Repositories;
using ChatSignalR.Core.Interfaces.Services;
using ChatSignalR.DataAccess.AzureSQL.Models;
using ChatSignalR.Server.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ITextAnalyticsService _textAnalyticsService;

        public MessageService(IMessageRepository messageRepository, ITextAnalyticsService textAnalyticsService)
        {
            _messageRepository = messageRepository;
            _textAnalyticsService = textAnalyticsService;
        }

        public async Task<Message?> Get(Guid id) => await _messageRepository.Get(id);

        public async Task<(bool created, MessageSentiment sentiment)> RegisterMessage(MessageCreateDto dto)
        {
            var analyticsResult = await _textAnalyticsService.AnalyzeMessage(dto.messageContent);
            var message = new Message() { AuthorName = dto.userName, 
                ChatName = dto.chatName, 
                MessageContent = dto.messageContent,
                PositiveScore = analyticsResult.positiveScore,
                NeutralScore = analyticsResult.neutralScore,
                NegativeScore = analyticsResult.negativeScore};
            return (await _messageRepository.Create(message) != Guid.Empty, analyticsResult.sentiment);
        }


    }
}
