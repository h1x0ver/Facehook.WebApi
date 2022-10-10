using Facehook.Business.DTO_s.User;
using Facehook.Entity.Entites;
using Facehook.Entity.Identity;
using Microsoft.AspNetCore.Http;

namespace Facehook.Entity.DTO.Post;
public class PostCreateDTO
{
    public string? Title { get; set; }
    public UserGetDTO? User { get; set; }
    public int LikeCount { get; set; }
    public ICollection<Like>? Likes { get; set; }
    public int CommentCount { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public List<IFormFile>? ImageFiles { get; set; }
}
