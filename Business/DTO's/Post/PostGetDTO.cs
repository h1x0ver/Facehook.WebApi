using Facehook.Business.DTO_s.Comment;
using Facehook.Business.DTO_s.User;

namespace Facehook.Entity.DTO.Post;
public class PostGetDto
{
    public int Id { get; set; }
    public List<string?>? ImageName { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? Title { get; set; }
    public UserGetDTO? User { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public List<CommentGetDTO>? Comments { get; set; }
}


