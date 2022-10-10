namespace Facehook.Business.Base;
public interface IBaseServiceForUsers<TGet, TUpdate, ProfilePhotoGet, TUserProfileDto>
{
    Task<TGet> Get(string id);
    Task<List<TGet>> GetAll();

    Task Update(TUpdate entity);
    Task ChangeProfilePhotoAsync(ProfilePhotoGet entity);
    Task<TUserProfileDto> GetUserProfileAsync(string? id);

}
