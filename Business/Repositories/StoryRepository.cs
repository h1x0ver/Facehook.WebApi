using AutoMapper;
using Facehook.Business.DTO_s;
using Facehook.Business.DTO_s.Story;
using Facehook.Business.Extensions;
using Facehook.Business.Helper;
using Facehook.Business.Services;
using Facehook.DAL.Abstracts;
using Facehook.Entity.Entites;
using Facehook.Entity.Identity;
using Facehook.Exceptions.EntityExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace Facehook.Business.Repositories;

public class StoryRepository : IStoryService
{
    private readonly IStoryDal _storyDal;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IMapper _mapper;
    private readonly IImageDal _imageDal;
    private readonly IUserDal _userDal;

    public StoryRepository(IStoryDal storyDal,
        IHttpContextAccessor httpContextAccessor,
        IHostEnvironment hostEnvironment,
        IMapper mapper,
        IImageDal imageDal,
        IUserDal userDal)
    {
        _storyDal = storyDal;
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
        _mapper = mapper;
        _imageDal = imageDal;
        _userDal = userDal;
    }

    public async Task<StoryGetDTO> Get(int id)
    {

        var data = await _storyDal.GetAsync(n => n.Id == id && !n.isDeleted, 0, "User.ProfileImage", "Images");
        if (data is null) throw new EntityCouldNotFoundException();
        return _mapper.Map<StoryGetDTO>(data);
    }

    public async Task<List<StoryGetDTO>> GetAll()
    {
        var datas = await _storyDal.GetAllAsync(orderBy: n => n.CreatedDate, n => !n.isDeleted, 0, int.MaxValue, "User.ProfileImage", "Images");
        if (datas is null) throw new EntityCouldNotFoundException();
        return _mapper.Map<List<StoryGetDTO>>(datas);
    }

    public async Task Create(StoryCreateDTO entity)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser appUser = await _userDal.GetAsync(u => u.Id == userId);
        var story = _mapper.Map<Story>(entity);
        story.UserId = userId;
        story.User = appUser;
        story.CreatedDate = DateTime.UtcNow.AddHours(4);
        if (entity.ImageFiles != null)
        {
            List<Image> images = new();
            foreach (var imageFile in entity.ImageFiles)
            {
                Image image = new()
                {
                    Name = await imageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images")
                };
                await _imageDal.CreateAsync(image);
                images.Add(image);
            }
            story.Images = images;
        }
        await _storyDal.CreateAsync(story);
    }

    public async Task Delete(int id)
    {
        Story story = await _storyDal.GetAsync(n => n.Id == id, includes: "Images");
        if (story == null) throw new NullReferenceException();
        //await _postDal.DeleteAsync(post);
        story.isDeleted = true;
        await _storyDal.SaveAsync();
    }

   
}
