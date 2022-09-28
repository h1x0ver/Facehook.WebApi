namespace Facehook.Entity.DTO.Post;
public class PostGetDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public List<string?>? ImageName { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsSave { get; set; } = false;
}
