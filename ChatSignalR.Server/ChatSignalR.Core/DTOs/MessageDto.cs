using ChatSignalR.DataAccess.AzureSQL.Models;

namespace ChatSignalR.Server.DTOs
{
    /// <summary>
    /// A data transfer model of the message to be sent to the client
    /// </summary>
    public record MessageDto
    {
        public string UserName { get; set; }
        public string MessageText { get; set; }
        public MessageSentiment Sentiment { get; set; }
        public DateTime SentTime { get; set; }


        public MessageDto(string? UserName, string? MessageText, MessageSentiment? Sentiment, DateTime? SentTime)
        {
            this.UserName = UserName ?? "unknown";
            this.MessageText = MessageText ?? "no data";
            this.Sentiment = Sentiment ?? MessageSentiment.Neutral;
            this.SentTime = SentTime ?? DateTime.MinValue;
        }

        public MessageDto(string? UserName, string? MessageText, DateTime? SentTime) : this(UserName, MessageText, null, SentTime) { }

        public MessageDto(Message message) : 
            this(message?.AuthorName, 
                message?.MessageContent, 
                message?.Sentiment,
                message?.Created)
        {  }
    }

}
