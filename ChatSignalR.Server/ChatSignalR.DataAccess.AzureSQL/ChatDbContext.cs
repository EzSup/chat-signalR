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
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Message>(new MessageConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        
    }
}
