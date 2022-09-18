
using Facehook.Entity.Base;

namespace Facehook.Entity.Entites;
public class Post : BaseEntity, IEntity
{
    public string? Title { get; set; }  
    public List<PostImage>? PostImages { get; set; }
}
