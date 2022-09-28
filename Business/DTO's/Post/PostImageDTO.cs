using Microsoft.AspNetCore.Http;

namespace Facehook.Business.DTO_s.Post;

public class PostImageDto
{
    public int Id { get; set; }
    public string? ImageName { get; set; }
    public int? PostId { get; set; } = null;
    public string? UserId { get; set; }
    public IFormFile? ImageFile { get; set; }
}
