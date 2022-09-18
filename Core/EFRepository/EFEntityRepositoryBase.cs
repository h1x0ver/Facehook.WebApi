using Facehook.Core.EFRepository.EFBase;
using Facehook.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Facehook.Core.EFRepository;
public class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepositoryBase<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext
{
    private readonly TContext _context;

    public EFEntityRepositoryBase(TContext context)
    {
        _context = context;
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? expression)
    {
        var data = expression == null ?
            await _context.Set<TEntity>().FirstOrDefaultAsync() :
            await _context.Set<TEntity>().Where(expression).FirstOrDefaultAsync();

#pragma warning disable CS8603 // Possible null reference return.
        return data;
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression)
    {
        var data = expression == null ?
            await _context.Set<TEntity>().ToListAsync() :
            await _context.Set<TEntity>().Where(expression).ToListAsync();

        return data;
    }
    public async Task CreateAsync(TEntity entity)
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Added;
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
