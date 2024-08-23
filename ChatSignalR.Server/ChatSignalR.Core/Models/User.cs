namespace ChatSignalR.DataAccess.AzureSQL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<Message>? Messages { get; set; }
    }
}
