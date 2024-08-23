namespace ChatSignalR.DataAccess.AzureSQL.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string? AuthorName { get; set; }
        public string? ChatName { get; set; }
        public string? MessageContent { get; set; }
        public DateTime? Created { get; set; } = DateTime.UtcNow;


        public double PositiveScore { get; set; }
        public double NeutralScore { get; set; }
        public double NegativeScore { get; set; }
        
    }
}
