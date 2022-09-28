using Facehook.Business.DTO_s.Post;
using Microsoft.AspNetCore.Http;

namespace Facehook.Entity.DTO.Post;
public class PostCreateDTO
{
    public string? Title { get; set; }
    public string? UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<PostImageDto>? Pictures { get; set; }
    public List<IFormFile>? ImageFiles { get; set; }
}
