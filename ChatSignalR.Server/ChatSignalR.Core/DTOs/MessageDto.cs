using ChatSignalR.DataAccess.AzureSQL.Models;

namespace ChatSignalR.Server.DTOs
{
    public record MessageDto
    {
        public string UserName { get; set; }
        public string MessageText { get; set; }
        public MessageSentiment Sentiment { get; set; }

        public MessageDto(string UserName, string MessageText, MessageSentiment Sentiment)
        {
            this.UserName = UserName;
            this.MessageText = MessageText;
            this.Sentiment = Sentiment;
        }

        public MessageDto(Message message)
        {
            this.UserName = message?.AuthorName ?? "unknown";
            this.MessageText = message?.MessageContent ?? "no data";
        }
    }

    public enum MessageSentiment
    {
        Positive, Neutral,  Negative, Mixed
    }
}
