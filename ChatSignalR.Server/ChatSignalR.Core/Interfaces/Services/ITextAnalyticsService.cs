using ChatSignalR.Server.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Core.Interfaces.Services
{
    public interface ITextAnalyticsService
    {
        Task<(MessageSentiment sentiment, double positiveScore, double neutralScore, double negativeScore)> AnalyzeMessage(string messageContent);
    }
}
