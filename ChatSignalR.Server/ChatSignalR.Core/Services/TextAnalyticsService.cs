using Azure;
using Azure.AI.TextAnalytics;
using ChatSignalR.Core.Interfaces.Services;
using ChatSignalR.DataAccess.AzureSQL.Models;
using ChatSignalR.Server.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Core.Services
{
    public class TextAnalyticsService : ITextAnalyticsService
    {
        private readonly TextAnalyticsClient _client;

        public TextAnalyticsService(string endpointUrl, string key)
        {
            Uri endpoint = new(endpointUrl);
            AzureKeyCredential credential = new(key);
            _client = new TextAnalyticsClient(endpoint, credential);
        }

        public async Task<(MessageSentiment sentiment, double positiveScore, double neutralScore, double negativeScore)> AnalyzeMessage(string messageContent)
        {
            if(String.IsNullOrWhiteSpace(messageContent))
            {
                throw new ArgumentNullException(nameof(messageContent));
            }
            Response<DocumentSentiment> response = _client.AnalyzeSentiment(messageContent);
            DocumentSentiment docSentiment = response.Value;
            return ((MessageSentiment)docSentiment.Sentiment, docSentiment.ConfidenceScores.Positive, docSentiment.ConfidenceScores.Neutral, docSentiment.ConfidenceScores.Negative);
            
        }
    }
}
