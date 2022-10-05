using AutoMapper;
using Facehook.Business.Base;
using Facehook.Business.DTO_s.User;
using Facehook.Business.Services;
using Facehook.DAL.Abstracts;
using Facehook.Entity.Entites;
using Facehook.Entity.Entites.Enum;
using Facehook.Entity.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Facehook.Business.Repositories;

public class UserFriendRepository : IUserFriendService
{
    private readonly IUserFriendDal _userFriendDal;
    private readonly IUserDal _userDal;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public UserFriendRepository(IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        IUserFriendDal userFriendDal,
        IUserDal userDal)
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _userFriendDal = userFriendDal;
        _userDal = userDal;
    }

    public async Task FriendAcceptAsync(string? friendId)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        UserFriend userFriend = await _userFriendDal.GetAsync(n => n.FriendId == userId && n.UserId == friendId);
        userFriend.Status = FriendRequestStatus.Accepted;
        await _userFriendDal.UpdateAsync(userFriend);
    }

    public async Task FriendAddAsync(string? friendId)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser user = await _userDal.GetAsync(u => u.Id == userId);
        if (user is null)
        {
            throw new NullReferenceException();
        }
        AppUser userfriends = await _userDal.GetAsync(u => u.Id == friendId);
        UserFriend userFriend = new()
        {
            UserId = userId,
            FriendId = userfriends?.Id,
            Status = FriendRequestStatus.Pending
        };
        await _userFriendDal.CreateAsync(userFriend);
    }

    public async Task<List<UserGetDTO>> FriendGetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task FriendRejectAsync(string? friendId)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        UserFriend userFriend = await _userFriendDal.GetAsync(n => n.UserId == userId && n.FriendId == friendId);
        if (userFriend is null)
        {
            throw new NullReferenceException();
        }
        await _userFriendDal.DeleteAsync(userFriend);
    }

    public async Task FriendRemoveAsync(string? friendId)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        UserFriend userFriend = await _userFriendDal.GetAsync(n => n.FriendId == userId && n.UserId == friendId);
        await _userFriendDal.DeleteAsync(userFriend);
    }

    public async Task<List<UserGetDTO>> GetFriendRequestAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<FriendSuggestionDTO>> GetFriendSuggestionAsync()
    {
        throw new NotImplementedException();
    }
}

//this 