using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Facehook.Entity.Entites;
using Microsoft.EntityFrameworkCore;

namespace Facehook.Entity.Configurations;

public class PostConfigurations : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(n => n.Title)
            .IsRequired(true);
        builder.Property(n => n.isDeleted)
            .HasDefaultValue(false);
        builder.Property(n => n.CreatedDate)
            .IsRequired(true)
            .HasDefaultValueSql("GETUTCDATE()");

    }
}
