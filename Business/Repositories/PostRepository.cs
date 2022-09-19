using Facehook.Business.Services;
using Facehook.DAL.Abstracts;
using Facehook.DAL.Context;
using Facehook.Entity.Entites;
using Facehook.Exceptions.EntityExceptions;
using Microsoft.EntityFrameworkCore;

namespace Facehook.Business.Repositories;
public class PostRepository : IPostService
{
    private readonly IPostDal _postDal;
    public PostRepository(IPostDal postDal)
    {
        _postDal = postDal;
    }

    public async Task<Post> Get(int id)
    {
        var data = await _postDal.GetAsync(n => n.Id == id && !n.isDeleted,"PostImages.Image");

        if (data is null)
        {
            throw new EntityCouldNotFoundException();
        }
        return data;

    }

    public async Task<List<Post>> GetAll()
    {
        var data = await _postDal.GetAllAsync(n => !n.isDeleted, "PostImages.Image");

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
