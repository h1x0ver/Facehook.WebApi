
using Facehook.Entity.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facehook.Entity.Configurations;

public class StoryConfigration : IEntityTypeConfiguration<Story>
{
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.Property(s => s.CreatedDate).HasDefaultValueSql("GETDATE()");
        builder.Property(s => s.isDeleted).HasDefaultValue(false);
    }
}