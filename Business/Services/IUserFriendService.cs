using Facehook.Business.Base;
using Facehook.Business.DTO_s.User;

namespace Facehook.Business.Services;

public interface IUserFriendService : IBaseServieForFriends<UserGetDTO, FriendSuggestionDTO>
{
}
