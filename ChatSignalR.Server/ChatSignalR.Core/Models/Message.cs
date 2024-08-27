﻿namespace ChatSignalR.DataAccess.AzureSQL.Models
{
    /// <summary>
    /// A model of the message to be stored in database
    /// </summary>
    public class Message
    {
        public Guid Id { get; set; }
        public string? AuthorName { get; set; }
        public string? ChatName { get; set; }
        public string? MessageContent { get; set; }
        public DateTime? Created { get; set; } = DateTime.UtcNow;

        public MessageSentiment Sentiment { get; set; }
        public double PositiveScore { get; set; }
        public double NeutralScore { get; set; }
        public double NegativeScore { get; set; }
        
    }

    public enum MessageSentiment
    {
        Positive, Neutral, Negative, Mixed
    }
}
