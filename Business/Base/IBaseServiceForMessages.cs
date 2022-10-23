using Facehook.Business.DTO_s.Message;

namespace Facehook.Business.Base;

public interface IBaseServiceForMessages<TGet>
{
    Task<TGet> SendMessage(MessageDTO message);
    Task<List<TGet>> GetMessages(string username);
}
