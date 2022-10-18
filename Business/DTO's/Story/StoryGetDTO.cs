using Facehook.Business.DTO_s.User;

namespace Facehook.Business.DTO_s;

public class StoryGetDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public UserGetDTO? User { get; set; }
    public List<string?>? ImageName { get; set; }
}
