namespace ChatSignalR.DataAccess.AzureSQL.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public Guid ChatId { get; set; }
        public string? MessageContent { get; set; }
        public DateTime? Created { get; set; } = DateTime.UtcNow;

        public User? Author { get; set; }
        public Chat? Chat { get; set; }
    }
}
