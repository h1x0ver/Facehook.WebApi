using Microsoft.AspNetCore.Http;

namespace Facehook.Entity.DTO.Post;
public class PostCreateDTO
{
    public string? Title { get; set; }

    public List<IFormFile>? ImageFiles { get; set; }
}
