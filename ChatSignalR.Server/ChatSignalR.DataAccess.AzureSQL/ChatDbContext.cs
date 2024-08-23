using ChatSignalR.DataAccess.AzureSQL.Configurations;
using ChatSignalR.DataAccess.AzureSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatSignalR.DataAccess.AzureSQL
{
    public class ChatDbContext : DbContext
    {

        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<Chat>(new ChatConfiguration());
            modelBuilder.ApplyConfiguration<Message>(new MessageConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
