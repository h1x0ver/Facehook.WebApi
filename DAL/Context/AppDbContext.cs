using Facehook.Entity.Entites;
using Microsoft.EntityFrameworkCore;
namespace Facehook.DAL.Context;
public class AppDbContext : DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public DbSet<Post> Posts { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<PostImage> PostImages{ get; set; }
}
