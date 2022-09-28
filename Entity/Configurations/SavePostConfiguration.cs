using Facehook.Entity.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facehook.Entity.Configurations;
public class SavePostConfiguration : IEntityTypeConfiguration<SavePost>
{
    public void Configure(EntityTypeBuilder<SavePost> builder)
    {
        builder.Property(n => n.CreatedDate).HasDefaultValueSql("GETDATE()");
        builder.Property(n => n.UserId).IsRequired();
        builder.Property(n => n.UserId).IsRequired();
    }
}
