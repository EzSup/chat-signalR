namespace ChatSignalR.DataAccess.AzureSQL.Models
{
    public class Chat
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public IEnumerable<Message>? Messages { get; set; }
    }
}
