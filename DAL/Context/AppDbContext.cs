using Facehook.Entity.Configurations;
using Facehook.Entity.Entites;
using Facehook.Entity.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Facehook.DAL.Context;
public class AppDbContext : IdentityDbContext<AppUser>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public DbSet<Post> Posts { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<SavePost> SavePosts{ get; set; }
    public DbSet<UserFriend> UserFriends { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AppUser>().HasMany(u => u.UserFriends).WithOne(uf => uf.User).HasForeignKey(uf => uf.UserId);
        builder.ApplyConfiguration(new PostConfigurations());
        builder.ApplyConfiguration(new StoryConfigration());
        builder.ApplyConfiguration(new CommentConfiguration());
        builder.ApplyConfiguration(new SavePostConfiguration());
        builder.ApplyConfiguration(new LikeConfiguration());
        base.OnModelCreating(builder);
    }
}
