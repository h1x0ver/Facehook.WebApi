namespace Facehook.Business.Base;

public interface IBaseServieForFriends<TGet, TSuggestion>
{
    Task FriendAddAsync(string? friendId);
    Task FriendAcceptAsync(string? friendId);
    Task FriendRemoveAsync(string? friendId);
    Task FriendRejectAsync(string? friendId);
    Task<List<TGet>> GetFriendRequestAsync();
    Task<List<TGet>> FriendGetAllAsync();
    Task<List<TSuggestion>> GetFriendSuggestionAsync();

}
