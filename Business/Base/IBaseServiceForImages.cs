namespace Facehook.Business.Base;

public interface IBaseServiceForImages<TEntity>
{
    Task<TEntity> Get(int id);
    Task<List<TEntity>> GetAll();
    Task Create(TEntity entity);
    Task Update(int id, TEntity entity);
    Task Delete(int id);
}
