using Facehook.Business.DTO_s.Post;
using Facehook.Entity.Base;
namespace Facehook.Business.Base;
public interface IBaseService<TGet, TCreate, TUpdate, TSave>
{
    Task<TGet> Get(int id);
    Task<List<TGet>> GetAll();
    Task Create(TCreate entity);
    Task Update(int id, TUpdate entity);
    Task Delete(int id);
    Task PostSave(int id);
    Task<List<TGet>> GetSavedPost();


}
