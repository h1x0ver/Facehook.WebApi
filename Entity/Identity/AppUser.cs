using Facehook.Entity.Base;
using Facehook.Entity.Entites;
using Microsoft.AspNetCore.Identity;

namespace Facehook.Entity.Identity;

public class AppUser : IdentityUser, IEntity
{
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string? Couuntry { get; set; }
    public string? Bio { get; set; }
    public string? Address { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime Birthday { get; set; }
    public ICollection<SavePost>? SavePosts { get; set; }
    public ICollection<Post>? Posts { get; set; }
    public ICollection<Comment>? Comments { get; set; }
}
