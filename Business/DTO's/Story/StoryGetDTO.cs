namespace Facehook.Business.DTO_s;

public class StoryGetDTO
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<string?>? ImageName { get; set; }
    public string? Title { get; set; }
}
