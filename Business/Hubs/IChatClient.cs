using Facehook.Business.DTO_s.Message;
using Facehook.Entity.Identity;

namespace Facehook.Business.Hubs;
public interface IChatClient
{
    Task ReceiveMessage(MessageGetDTO message);
    Task GetClients(List<AppUser> clients);
    Task GetConnectionId(string connectionId);
    Task GetClient(AppUser user);
}
