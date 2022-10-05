using Facehook.Entity.Base;
using Facehook.Entity.Identity;
namespace Facehook.Entity.Entites;
public class SavePost : IEntity
{
    public int Id { get; set; }
    public string? CreatedDate { get; set; }
    public string? UserId { get; set; }
    public AppUser? AppUser { get; set; }
    public int PostId { get; set; }
    public Post? Post { get; set; }
}
