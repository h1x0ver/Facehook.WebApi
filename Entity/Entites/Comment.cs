using Facehook.Entity.Base;
using Facehook.Entity.Identity;

namespace Facehook.Entity.Entites;
public class Comment : IEntity
{
    public int Id { get; set; }
    public string? Message { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? UserId { get; set; }
    public AppUser? User { get; set; }
    public int PostId { get; set; }
    public Post? Post { get; set; }
    public int? CommentId { get; set; }
    public ICollection<Comment>? ReplyComment { get; set; }
}
