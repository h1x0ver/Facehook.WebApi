using Facehook.Entity.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facehook.Entity.Configurations;
public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(c => c.CreatedDate).HasDefaultValueSql("GETDATE()");
        builder.Property(c => c.Message).IsRequired();
    }
}
