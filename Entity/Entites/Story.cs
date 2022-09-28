using Facehook.Entity.Base;
using Facehook.Entity.Identity;

namespace Facehook.Entity.Entites;
public class Story : BaseEntity, IEntity
{
    public string? Content { get; set; }
    public string? StoryFileName { get; set; }
    public int UserId { get; set; }
    public AppUser? User { get; set; }
}
