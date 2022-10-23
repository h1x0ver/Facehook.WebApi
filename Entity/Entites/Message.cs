using Facehook.Entity.Base;
using Facehook.Entity.Identity;

namespace Facehook.Entity.Entites;

public class Message : IEntity
{
    public int Id { get; set; }
    public string? Content { get; set; }
    public DateTime SenderDate { get; set; }
    public bool IsDeleted { get; set; }
    public string? SendUserId { get; set; }
    public AppUser? SendUser { get; set; }
    public string? UserId { get; set; }
    public AppUser? User { get; set; }
}
