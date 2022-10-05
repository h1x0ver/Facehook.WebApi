using AutoMapper;
using Facehook.Business.DTO_s;
using Facehook.Business.DTO_s.Story;
using Facehook.Business.Extensions;
using Facehook.Business.Helper;
using Facehook.Business.Services;
using Facehook.DAL.Abstracts;
using Facehook.Entity.Entites;
using Facehook.Exceptions.EntityExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Facehook.Business.Repositories;

public class StoryRepository : IStoryService
{
    private readonly IStoryDal _storyDal;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IMapper _mapper;
    private readonly IImageDal _imageDal;

    public StoryRepository(IStoryDal storyDal,
        IHttpContextAccessor httpContextAccessor,
        IHostEnvironment hostEnvironment,
        IMapper mapper,
        IImageDal imageDal)
    {
        _storyDal = storyDal;
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
        _mapper = mapper;
        _imageDal = imageDal;
    }

    public async Task<StoryGetDTO> Get(int id)
    {
        var data = await _storyDal.GetAsync(n => n.Id == id && !n.isDeleted, includes: "Images");
        if (data is null)
        {
            throw new EntityCouldNotFoundException();
        }
        List<string?> imageUrls = new();
        imageUrls.AddRange(data.Images!.Where(n => n.PostId == data.Id).ToList().Select(storyImage => storyImage.Name));

        var postGetDto = _mapper.Map<StoryGetDTO>(data);
        postGetDto.ImageName = imageUrls;
        return postGetDto;
    }

    public async Task<List<StoryGetDTO>> GetAll()
    {
        var datas = await _storyDal.GetAllAsync(n => !n.isDeleted, includes: "Images");

        if (datas is null)
        {
            throw new EntityCouldNotFoundException();
        }
        List<StoryGetDTO> storyGetDTOs = new();

        foreach (var data in datas)
        {
            List<string?> imageUrls = new();
            imageUrls.AddRange(data.Images!.Where(n => n.StoryId == data.Id).ToList().Select(storyImage => storyImage.Name));
            StoryGetDTO storyGetDTO = new()
            {
                Id = data.Id,
                Title = data.Title,
                CreatedDate = data.CreatedDate,
                ImageName = imageUrls
            };

            storyGetDTOs.Add(storyGetDTO);
        }
        return storyGetDTOs;
    }

    public async Task Create(StoryCreateDTO entity)
    {
        var story = _mapper.Map<Story>(entity);
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

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

   
}
