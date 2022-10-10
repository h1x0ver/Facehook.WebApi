using Facehook.Business.DTO_s.User;
using Facehook.Entity.Identity;

namespace Facehook.Business.DTO_s.Comment;

public class CommentGetDTO
{
    public string? Content { get; set; }
    public int? PostId { get; set; }
    public UserGetDTO User { get; set; }
}
