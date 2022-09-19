using Facehook.Core.EFRepository.EFBase;
using Facehook.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Facehook.Core.EFRepository;

//this class is fundament core project

public class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepositoryBase<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext
{
    private readonly TContext _context;

    public EFEntityRepositoryBase(TContext context)
    {
        _context = context;
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? expression, params string[] includes)
    {
        var query = expression == null ?
          _context.Set<TEntity>().AsNoTracking() :
          _context.Set<TEntity>().Where(expression).AsNoTracking();

        if (includes != null)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        var data = await query.FirstOrDefaultAsync();

#pragma warning disable CS8603 // Possible null reference return.
        return data;
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression, params string[] includes)
    {
        var query = expression == null ?
            _context.Set<TEntity>().AsNoTracking() :
            _context.Set<TEntity>().Where(expression).AsNoTracking();

        if (includes != null)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        var data = await query.ToListAsync();

        return data;
    }
    public async Task CreateAsync(TEntity entity)
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Added;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }
}
