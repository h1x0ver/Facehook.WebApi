using Facehook.Entity.Base;
using Facehook.Entity.Identity;

namespace Facehook.Entity.Entites;
public class Like : IEntity
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public AppUser? AppUser { get; set; }
    public int PostId { get; set; }
    public Post? Post { get; set; }
    public DateTime LikedDate { get; set; }
}
