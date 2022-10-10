using Facehook.Entity.Entites.Enum;

namespace Facehook.Business.DTO_s.User;

public class UserProfileDTO
{
    public string? Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? UserName { get; set; }
    public string? ProfileImage { get; set; }
    public int FriendCount { get; set; }
    public int PostCount { get; set; }
    public FriendRequestStatus Status { get; set; }
}
