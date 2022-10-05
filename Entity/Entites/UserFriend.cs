using Facehook.Entity.Base;
using Facehook.Entity.Entites.Enum;
using Facehook.Entity.Identity;

namespace Facehook.Entity.Entites;

public class UserFriend : IEntity
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public AppUser? User { get; set; }
    public string? FriendId { get; set; }
    public AppUser? Friend { get; set; }
    public DateTime SenderDate { get; set; }
    public FriendRequestStatus Status { get; set; }

}
