using AutoMapper;
using Facehook.Business.DTO_s.Comment;
using Facehook.Business.DTO_s.Post;
using Facehook.Business.DTO_s.User;
using Facehook.Business.Exceptions;
using Facehook.Business.Extensions;
using Facehook.Business.Helper;
using Facehook.Business.Services;
using Facehook.DAL.Abstracts;
using Facehook.Entity.DTO.Post;
using Facehook.Entity.Entites;
using Facehook.Entity.Identity;
using Facehook.Exceptions.EntityExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace Facehook.Business.Repositories;
public class PostRepository : IPostService
{
    private readonly IPostDal _postDal;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IMapper _mapper;
    private readonly IImageDal _imageDal;
    private readonly IUserDal _userDal;
    private readonly ISavePostDal _savePostDal;
    public PostRepository(IPostDal postDal,
        IHttpContextAccessor httpContextAccessor,
        IHostEnvironment hostEnvironment,
        IMapper mapper,
        IImageDal imageDal,
        IUserDal userDal,
        ISavePostDal savePostDal)
    {
        _postDal = postDal;
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
        _mapper = mapper;
        _imageDal = imageDal;
        _userDal = userDal;
        _savePostDal = savePostDal;
    }
    public async Task<PostGetDto> Get(int id)   
    {

        var data = await _postDal.GetAsync(n => n.Id == id && !n.isDeleted, 0, "User.ProfileImage", "Likes", "Comments", "Comments.User", "Comments.User.ProfileImage", "Images");
        if (data is null) throw new EntityCouldNotFoundException();
        return _mapper.Map<PostGetDto>(data);
    }

    public async Task<List<PostGetDto>> GetAll()
    {
        var datas = await _postDal.GetAllAsync(n => !n.isDeleted, 0, int.MaxValue, "User.ProfileImage", "Likes", "Comments", "Comments.User", "Comments.User.ProfileImage", "Images");
        if (datas is null) throw new EntityCouldNotFoundException();
        return _mapper.Map<List<PostGetDto>>(datas);
    }
    public async Task Create(PostCreateDTO entity)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser appUser = await _userDal.GetAsync(u => u.Id == userId);
        var post = _mapper.Map<Post>(entity);
        post.UserId = userId;
        post.User = appUser;
        post.CreatedDate = DateTime.UtcNow.AddHours(4);
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
            post.Images = images;
        }
        await _postDal.CreateAsync(post);
    }

    public Task Update(int id, PostUpdateDTO entity)
    {
        //adam postu paylasanda fikirlesin, update etmke muveqqeti olarag mumkun deyl
        throw new NotImplementedException();
    }
    public async Task Delete(int id)
    {
        Post post = await _postDal.GetAsync(n => n.Id == id , includes: "Images");
        if (post == null) throw new NullReferenceException();
        //await _postDal.DeleteAsync(post);
        post.isDeleted = true;
        await _postDal.SaveAsync();
    }
    public async Task PostSave(PostSaveDTO entity)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser appUser = await _userDal.GetAsync(u => u.Id == userId);
        var postDb = await _postDal.GetAsync(p => p.Id == entity.PostId);
        if (postDb is null)
        {
            throw new NotFoundException("Post is not found");
        };
        if (entity.IsSave)
        {
            var savePost = new SavePost
            {
                PostId = entity.PostId,
                UserId = userId
            };
            await _savePostDal.CreateAsync(savePost);
        }
        else
        {
            SavePost posSaveDb = await _savePostDal.GetAsync(s => s.UserId == userId && s.PostId == entity.PostId);
            if (posSaveDb is null)
            {
                throw new NotFoundException("Post is not found");
            }
            await _savePostDal.DeleteAsync(posSaveDb);
        }
        await _savePostDal.SaveAsync();
    }

    public async Task<List<PostGetDto>> GetSavedPost()
    {   
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser appUser = await _userDal.GetAsync(u => u.Id == userId);
        var savedPostsOfUsers = await _savePostDal.GetAllAsync(s => s.UserId == appUser.Id);
        var savedPostIds = savedPostsOfUsers.Select(s => s.PostId);
        var savedPosts = await _postDal.GetAllAsync(p => !p.isDeleted && savedPostIds.Contains(p.Id), 0, int.MaxValue, "User.ProfileImage", "Likes", "Comments", "Comments.User", "Comments.User.ProfileImage", "Images");
        return _mapper.Map<List<PostGetDto>>(savedPosts);
    }
}
