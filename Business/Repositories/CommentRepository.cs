using AutoMapper;
using Facehook.Business.DTO_s;
using Facehook.Business.DTO_s.Comment;
using Facehook.Business.Services;
using Facehook.DAL.Abstracts;
using Facehook.Entity.Entites;
using Facehook.Entity.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Facehook.Business.Repositories;

public class CommentRepository : ICommentService
{
    private readonly ICommentDal _commentDal;
    private readonly IUserDal _userDal;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public CommentRepository(ICommentDal commentDal,
                             IUserDal userDal,
                             IHttpContextAccessor httpContextAccessor,
                             IMapper mapper)
    {
        _commentDal = commentDal;
        _userDal = userDal;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task CreateCommentAsync(CommentCreateDTO entity)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        AppUser appUser = await _userDal.GetAsync(u => u.Id == userId);
        Comment comment = _mapper.Map<Comment>(entity);
        comment.UserId = userId;
        comment.User = appUser;
        await _commentDal.CreateAsync(comment);
    }

    public async Task DeleteCommentAsync(int id)
    {
        Comment comment = await _commentDal.GetAsync(u => u.Id == id);
        if (comment == null) throw new NullReferenceException();
        await _commentDal.DeleteAsync(comment);
    }

    public async Task<List<CommentGetDTO>> GetPostCommentsAsync(int id)
    {
        List<Comment> comments = await _commentDal.GetAllAsync(n => n.PostId == id, 0, int.MaxValue, "User.ProfileImage");
        List<CommentGetDTO> commentGetDtos = _mapper.Map<List<CommentGetDTO>>(comments);
        return commentGetDtos;
    }
}
