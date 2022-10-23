using Facehook.Business.DTO_s.User;

namespace Facehook.Business.DTO_s.Message;

public class MessageGetDTO
{
    public int Id { get; set; }
    public string? Content { get; set; }
    public DateTime SenderDate { get; set; }
    public UserGetDTO? SendUser { get; set; }
}
