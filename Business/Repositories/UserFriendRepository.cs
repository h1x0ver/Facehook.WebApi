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
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        List<UserFriend> userFriends = await _userFriendDal.GetAllAsync(n => n.Id, n => (n.FriendId == userId && n.Status == FriendRequestStatus.Accepted) || (n.UserId == userId && n.Status == FriendRequestStatus.Accepted),0, int.MaxValue, "User.ProfileImage", "Friend.ProfileImage");
        if (userFriends is null) throw new NullReferenceException();
        List<UserGetDTO> userGetDtos = new();
        foreach (var userFriend in userFriends)
        {
            UserGetDTO userGetDto = new();
            if (userFriend.UserId == userId)
            {
                userGetDto = _mapper.Map<UserGetDTO>(userFriend.Friend);
                userGetDto.ProfileImage = userFriend.Friend?.ProfileImage?.Name;
            }
            else if (userFriend.FriendId == userId)
            {
                userGetDto = _mapper.Map<UserGetDTO>(userFriend.User);
                userGetDto.ProfileImage = userFriend.User?.ProfileImage?.Name;

            }
            userGetDtos.Add(userGetDto);
        }
        return userGetDtos;
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
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        List<UserFriend> userFriends = await _userFriendDal.GetAllAsync(orderBy:n=>n.SenderDate,n => n.FriendId == userId && n.Status == FriendRequestStatus.Pending, includes:"User.ProfileImage");
        if (userFriends is null) throw new NullReferenceException();
        List<UserGetDTO> userGetDtos = new();
        foreach (var userFriend in userFriends)
        {
            UserGetDTO userGetDto = _mapper.Map<UserGetDTO>(userFriend.User);
            userGetDto.ProfileImage = userFriend.User?.ProfileImage?.Name;
            userGetDtos.Add(userGetDto);
        }
        return userGetDtos;
    }

    public async Task<List<FriendSuggestionDTO>> GetFriendSuggestionAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        List<AppUser> users = await _userDal.GetAllAsync(orderBy:n=>n.Id,expression: u => u.Id != userId, includes: "ProfileImage");
        if (users is null) throw new NullReferenceException();
        List<AppUser> notFriends = new();
        foreach (var user in users)
        {
            UserFriend userFriend = await _userFriendDal.GetAsync(u => (u.UserId == user.Id && u.FriendId == userId) || (u.FriendId == user.Id && u.UserId == userId));
            if (userFriend is null)
            {
                notFriends.Add(user);
            }
        }
        List<FriendSuggestionDTO> friendSuggestionDtos = _mapper.Map<List<FriendSuggestionDTO>>(notFriends);
        for (int i = 0; i < notFriends.Count; i++)
        {
            friendSuggestionDtos[i].ImageUrl = notFriends[i].ProfileImage?.Name;
        }

        return friendSuggestionDtos;
    }
}

//this 