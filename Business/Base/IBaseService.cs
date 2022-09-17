using Entity.Base;

namespace Business.Base;
public interface IBaseService<TEntity>
    where TEntity : class, IEntity, new()
{
    Task<TEntity> Get(int id);
    Task <List<TEntity>> GetAll();
    Task Create(TEntity entity);
    Task<TEntity> Update(int id,TEntity entity);
    Task Delete(int id);
}
