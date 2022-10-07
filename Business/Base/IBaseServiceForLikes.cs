namespace Facehook.Business.Base;

public interface IBaseServiceForLikes
{
    Task AddLikeAsync(int id);
    Task DeleteLikeAsync(int id);
}
