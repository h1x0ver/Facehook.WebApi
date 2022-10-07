using Facehook.Entity.Entites;
using Facehook.Entity.Identity;

namespace Facehook.Entity.DTO.Post;
public class PostGetDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public List<string?>? ImageName { get; set; }
    public DateTime CreatedDate { get; set; }
    public AppUser? User { get; set; }
    public int LikeCount { get; set; }
    public ICollection<Like>? Likes { get; set; }
    public int CommentCount { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public bool IsSave { get; set; } = false;
}
