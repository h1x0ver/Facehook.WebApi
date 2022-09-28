using Aniverse.Business.Exceptions.FileExceptions;
using AutoMapper;
using Facehook.Business.DTO_s.Post;
using Facehook.Business.Extensions;
using Facehook.Business.Helper;
using Facehook.Business.Services;
using Facehook.DAL.Abstracts;
using Facehook.Entity.DTO.Post;
using Facehook.Entity.Entites;
using Facehook.Exceptions.EntityExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Facehook.Business.Repositories;
public class PostRepository : IPostService
{
    private readonly IPostDal _postDal;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IMapper _mapper;
    public PostRepository(IPostDal postDal,
        IHttpContextAccessor httpContextAccessor,
        IHostEnvironment hostEnvironment,
        IMapper mapper)
    {
        _postDal = postDal;
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
        _mapper = mapper;
    }

    public async Task<PostGetDto> Get(int id)
    {
        var data = await _postDal.GetAsync(n => n.Id == id && !n.isDeleted, includes: "PostImages.Image");
        if (data is null)
        {
            throw new EntityCouldNotFoundException();
        }
        List<string?> imageUrls = new();
        imageUrls.AddRange(data.PostImages!.Where(n => n.PostId == data.Id).ToList().Select(productImage => productImage.Image!.Name));


        var postGetDto = _mapper.Map<PostGetDto>(data);
        postGetDto.ImageName = imageUrls;
        return postGetDto;
    }

    public async Task<List<PostGetDto>> GetAll()
    {
        var datas = await _postDal.GetAllAsync(n => !n.isDeleted, includes: "PostImages.Image");

        if (datas is null)
        {
            throw new EntityCouldNotFoundException();
        }
        List<PostGetDto> postGetDtos = new();

        foreach (var data in datas)
        {
            List<string?> imageUrls = new();
            imageUrls.AddRange(data.PostImages!.Where(n => n.PostId == data.Id).ToList().Select(postImage => postImage.Image!.Name));
            PostGetDto postGetDto = new()
            {
                Id = data.Id,
                Title = data.Title,
                CreatedDate = data.CreatedDate,
                ImageName = imageUrls
            };

            postGetDtos.Add(postGetDto);
        }

        return postGetDtos;
    }
    public async Task Create(PostCreateDTO entity)
    {
        var userLoginId = _httpContextAccessor?.HttpContext?.User?.GetUserId();
        entity.UserId = userLoginId;
        entity.CreatedDate = DateTime.UtcNow.AddHours(4);
        Post postCreateDto = _mapper.Map<Post>(entity);
        if (entity.ImageFiles != null)
        {
            PostImage postImage = new();
            foreach (var imageFile in entity.ImageFiles)
            {
                Image image = new()
                {
                    Name = await imageFile.FileSaveAsync(_hostEnvironment.ContentRootPath, "Images")
                };
                postImage.Image = image;
                postCreateDto.PostImages.Add(postImage);
            }
        }
        await _postDal.CreateAsync(postCreateDto);


    }

    public Task Update(int id, PostUpdateDTO entity)
    {
        throw new NotImplementedException();
    }
    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}
