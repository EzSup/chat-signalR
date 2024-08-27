using ChatSignalR.DataAccess.AzureSQL.Models;
using ChatSignalR.Server.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Core.Interfaces.Services
{
    /// <summary>
    /// A service to process text
    /// </summary>
    public interface ITextAnalyticsService
    {
        /// <summary>
        /// Analyzes sentiment of the given message content
        /// </summary>
        /// <param name="messageContent">The content of the message to analyze</param>
        /// <returns>A tuple containing the sentiment of the message, as well as the scores for positive, neutral, and negative sentiments.</returns>
        Task<(MessageSentiment sentiment, double positiveScore, double neutralScore, double negativeScore)> AnalyzeMessage(string messageContent);
    }
}
