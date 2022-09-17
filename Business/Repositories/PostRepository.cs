using Business.Services;
using DAL.Context;
using Entity.Entites;
using Exceptions.EntityExceptions;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories;
public class PostRepository : IPostService
{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Post> Get(int id)
    {
        var data = await _context.Posts.Where(n => n.Id == id && !n.isDeleted).FirstOrDefaultAsync();

        if (data is null)
        {
            throw new EntityCouldNotFoundException();
        }
        return data;

    }

    public async Task<List<Post>> GetAll()
    {
        var data = await _context.Posts.Where(n => !n.isDeleted).ToListAsync();

        if (data is null)
        {
            throw new EntityCouldNotFoundException();
        }
        return data;
    }

    public Task Create(Post entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Post> Update(int id, Post entity)
    {
        throw new NotImplementedException();
    }
}
