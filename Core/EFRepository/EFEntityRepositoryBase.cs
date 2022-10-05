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
    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? expression, int? skip = 0, params string[] includes)
    {
        var query = expression == null ?
          _context.Set<TEntity>() :
          _context.Set<TEntity>().Where(expression);

        
        if (skip != null)
        {
            query.Skip((int)skip);
        }

        
        if (includes != null)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        var data = await query.FirstOrDefaultAsync();
        if (data is null) throw new Exception();

        return data;
    }
    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression, int? skip = 0, int? take = int.MaxValue, params string[] includes)
    {
        var query = expression == null ?
            _context.Set<TEntity>().AsNoTracking() :
            _context.Set<TEntity>().Where(expression).AsNoTracking();

        if (skip != null)
        {
            query.Skip((int)skip);
        }

        if (take != null)
        {
            query.Take((int)take);
        }

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
    public async Task SaveAsync()
    {
       await _context.SaveChangesAsync();
    }


}
