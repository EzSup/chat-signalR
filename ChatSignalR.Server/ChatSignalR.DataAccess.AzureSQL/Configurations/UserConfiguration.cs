using ChatSignalR.DataAccess.AzureSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatSignalR.DataAccess.AzureSQL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Messages)
                .WithOne(mes => mes.Author)
                .HasForeignKey(x => x.AuthorId);
        }
    }
}
