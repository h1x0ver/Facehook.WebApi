using Facehook.Business.Services;
using Facehook.DAL.Abstracts;
using Facehook.Entity.Entites;
using Facehook.Entity.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Facehook.Business.Repositories;

public class LikeRepository : ILikeService
{
    private readonly IPostDal _postDal;
    private readonly ILikeDal _likeDal;
    private readonly IUserDal _userDal;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public LikeRepository(ILikeDal likeDal,
                          IHttpContextAccessor httpContextAccessor,
                          IUserDal userDal,
                          IPostDal postDal)
    {
        _likeDal = likeDal;
        _httpContextAccessor = httpContextAccessor;
        _userDal = userDal;
        _postDal = postDal;
    }

    public async Task AddLikeAsync(int id)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser appUser = await _userDal.GetAsync(u => u.Id == userId);
        Post post = await _postDal.GetAsync(p => p.Id == id);

        Like like = new()
        {
            UserId = userId,
            User = appUser,
            PostId = id,
            Post = post
        };

        await _likeDal.CreateAsync(like);
    }

    public async Task DeleteLikeAsync(int id)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        Like like = await _likeDal.GetAsync(l => l.UserId == userId && l.PostId == id);
        await _likeDal.DeleteAsync(like);
    }
}
