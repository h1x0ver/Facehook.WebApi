using Microsoft.AspNetCore.Http;

namespace Facehook.Business.DTO_s.Story;

public class StoryCreateDTO 
{
    public string? Title { get; set; }
    public List<IFormFile>? ImageFiles { get; set; }
}
