using Facehook.Entity.Base;
using System.Linq.Expressions;

namespace Facehook.Core.EFRepository.EFBase;
public interface IEntityRepositoryBase<TEntity>
    where TEntity : class, IEntity, new()
{
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? expression);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
