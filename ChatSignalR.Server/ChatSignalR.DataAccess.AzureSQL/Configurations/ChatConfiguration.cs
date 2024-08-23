using ChatSignalR.DataAccess.AzureSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatSignalR.DataAccess.AzureSQL.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(chat => chat.Messages)
                .WithOne(mes => mes.Chat)
                .HasForeignKey(mes => mes.ChatId);
        }
    }
}
