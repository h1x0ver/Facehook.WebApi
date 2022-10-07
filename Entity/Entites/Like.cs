using Facehook.Entity.Base;
using Facehook.Entity.Identity;

namespace Facehook.Entity.Entites;
public class Like : BaseEntity, IEntity
{
    public string? UserId { get; set; }
    public AppUser? User { get; set; }
    public int PostId { get; set; }
    public Post? Post { get; set; }
}
