namespace Entity.Entites;
public class PostImage
{
    public int Id { get; set; }
    public int ImageId { get; set; }
    public int PostId { get; set; }
    public Post? Post { get; set; }
    public Image? Image { get; set; }
}
