
using Facehook.Entity.Base;
using Facehook.Entity.Identity;

namespace Facehook.Entity.Entites;
public class Post : BaseEntity, IEntity
{
    public string? Title { get; set; }
    public string? UserId { get; set; }
    public AppUser? User { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Like>? Likes { get; set; }
    public ICollection<Image>? Images { get; set; }


}
