using AutoMapper;
using Facehook.Business.DTO_s.User;
using Facehook.Business.Services;
using Facehook.DAL.Abstracts;
using Facehook.Entity.Entites;
using Facehook.Entity.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using Facehook.Business.Helper;
using Facehook.Entity.Entites.Enum;

namespace Facehook.Business.Repositories;

public class UserRepository : IUserService


{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserDal _userDal;
    private readonly IImageDal _imageDal;
    private readonly IUserFriendDal _userFriendDal;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _hostEnvironment;

    public UserRepository(IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IHostEnvironment hostEnvironment,
        IUserDal userDal,
        UserManager<AppUser> userManager,
        IImageDal imageDal,
        IUserFriendDal userFriendDal)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
        _userDal = userDal;
        _userManager = userManager;
        _imageDal = imageDal;
        _userFriendDal = userFriendDal;
    }
    public async Task ChangeProfilePhotoAsync(ProfilePhotoDTO entity)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser appUser = await _userDal.GetAsync(u => u.Id == userId, includes: "ProfileImage");
        if (appUser is null) throw new NullReferenceException();
        var image = new Image
        {
            Name = await entity.ImageFile!.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images")
        };
        
        await _imageDal.CreateAsync(image);
        appUser.ProfileImage = image;
        appUser.ProfileImageId = image.Id;
        await _userDal.SaveAsync();
    }
    public async Task<UserGetDTO> Get(string id)         
    {
        AppUser appUser = await _userDal.GetAsync(u => u.Id == id,0,"ProfileImage","UserFriends");
        if (appUser is null) throw new NullReferenceException();
        return _mapper.Map<UserGetDTO>(appUser);

    }

    public async Task<List<UserGetDTO>> GetAll()
    {
        return _mapper.Map<List<UserGetDTO>>(await _userDal.GetAllAsync(orderBy: n => n.UserName, c => !c.IsDeleted, 0, int.MaxValue,includes: "ProfileImage"));
    }

    public async Task<UserProfileDTO> GetUserProfileAsync(string? id)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser user = await _userDal.GetAsync(u => u.Id == id,0, "ProfileImage", "Posts");
        UserProfileDTO userProfileDto = _mapper.Map<UserProfileDTO>(user);
        UserFriend userFriend = await _userFriendDal.GetAsync(u => (u.UserId == id && u.FriendId == userId) || (u.FriendId == id && u.UserId == userId));
        List<UserFriend> userFriends = await _userFriendDal.GetAllAsync(orderBy: n => n.UserId, u => (u.UserId == userId && u.Status == FriendRequestStatus.Accepted) || (u.FriendId == userId && u.Status == FriendRequestStatus.Accepted));
        userProfileDto.ProfileImage = user.ProfileImage is not null ? user.ProfileImage.Name : "";
        userProfileDto.PostCount = user.Posts is null ? 0 : user.Posts.Count;
        userProfileDto.FriendCount = userFriends is null ? 0 : userFriends.Count;
        if (userFriend is null)
        {
            userProfileDto.Status = FriendRequestStatus.NotFriend;
        }
        else if (userFriend.UserId == userId && userFriend.Status != FriendRequestStatus.Accepted)
        {
            userProfileDto.Status = FriendRequestStatus.Pending;
        }
        else if (userFriend.Status == FriendRequestStatus.Accepted)
        {
            userProfileDto.Status = FriendRequestStatus.Accepted;
        }
        else if (userFriend.FriendId == userId && userFriend.Status != FriendRequestStatus.Accepted)
        {
            userProfileDto.Status = FriendRequestStatus.Declined;
        }
     
        return userProfileDto;
    }

    public async Task Update(UserUpdateDTO entity)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser appUser = await _userDal.GetAsync(u => u.Id == userId);
        if (appUser is null) throw new NullReferenceException();
        AppUser checkUsername = await _userDal.GetAsync(u => u.UserName == entity.Username);
        if (checkUsername is not null) throw new ArgumentException();

        if (entity.Firstname is not null && entity.Firstname?.Trim() != "")
        {
            appUser.Firstname = entity.Firstname?.Trim();
        }

        if (entity.Lastname is not null && entity.Lastname?.Trim() != "")
        {
            appUser.Lastname = entity.Lastname?.Trim(); 
        }

        if (entity.Username is not null && entity.Username?.Trim() != "")
        {
            appUser.UserName = entity.Username?.Trim();
        }
        if (entity.Email is not null && entity.Email?.Trim() != "")
        {
            appUser.Email = entity.Email?.Trim();
        }

        await _userManager.UpdateAsync(appUser);
    }
    public async Task ChangeUserPasswordAsync(PasswordChangeDto passwordChangeDto)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser user = await _userDal.GetAsync(u => u.Id == userId, includes:"ProfileImage");
        if (!await _userManager.CheckPasswordAsync(user, passwordChangeDto.OldPassword)) throw new NullReferenceException();
        if (passwordChangeDto.NewPassword?.Trim() != passwordChangeDto.ConfirmPassword?.Trim())
        {
            throw new NullReferenceException();
        }
        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, passwordChangeDto.NewPassword);
    }
}
