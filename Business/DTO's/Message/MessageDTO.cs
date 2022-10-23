namespace Facehook.Business.DTO_s.Message;
public class MessageDTO
{
    public string? Content { get; set; }
    public string? SendUserId { get; set; }
    public DateTime SenderDate { get; set; } = DateTime.Now;
}
