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

namespace Facehook.Business.Repositories;

public class UserRepository : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserDal _userDal;
    private readonly IImageDal _imageDal;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _hostEnvironment;

    public UserRepository(IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IHostEnvironment hostEnvironment,
        IUserDal userDal,
        UserManager<AppUser> userManager,
        IImageDal imageDal)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
        _userDal = userDal;
        _userManager = userManager;
        _imageDal = imageDal;
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
        AppUser appUser = await _userDal.GetAsync(u => u.Id == id, includes: "ProfileImage");

        if (appUser is null)
        {
            throw new NullReferenceException();
        }

        UserGetDTO userGetDTO = new()
        {
            Id = appUser.Id,
            Username = appUser.UserName,
            Firstname = appUser.Firstname,
            Lastname = appUser.Lastname
        };
        userGetDTO.ProfileImage = appUser.ProfileImage is not null ? appUser.ProfileImage.Name : "";
        return userGetDTO;
    }
    public Task<List<UserGetDTO>> GetAll()
    {
        //mence getall a ehtiyac yoxdur bu case de
        throw new NotImplementedException();
        
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

        await _userManager.UpdateAsync(appUser);
    }
}
