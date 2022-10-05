using Facehook.Entity.Base;

namespace Facehook.Entity.Entites;
public class Image : IEntity
{
    public int Id { get; set; }
    public  string? Name { get; set; }
    public int? PostId { get; set; }
    public Post? Post { get; set; }
    public int? StoryId { get; set; }
    public Story? Story { get; set; }
}
