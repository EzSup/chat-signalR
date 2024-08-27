namespace ChatSignalR.Client.Models
{
    public record ChatMessage(string UserName, string MessageText, MessageSentiment Sentiment, DateTime SentTime);

    public enum MessageSentiment
    {
        Positive, Neutral, Negative, Mixed
    }
}
