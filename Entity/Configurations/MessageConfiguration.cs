using Facehook.Entity.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facehook.Entity.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(m => m.SenderDate)
               .HasDefaultValueSql("GETUTCDATE()");
        builder.Property(m => m.Content)
               .IsRequired();
        builder.Property(m => m.IsDeleted)
               .HasDefaultValue(false);
    }
}
