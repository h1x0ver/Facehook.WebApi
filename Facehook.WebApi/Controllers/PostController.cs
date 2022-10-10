using Facehook.Business.DTO_s;
using Facehook.Business.DTO_s.Post;
using Facehook.Business.Services;
using Facehook.Entity.DTO.Post;
using Facehook.Exceptions.EntityExceptions;
using Facehook.WebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Facehook.WebApi.Controllers;

[Route("api/[controller]"), ApiController, Authorize]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly ILikeService _likeService;
    private readonly ICommentService _commentService;

    public PostController(IPostService postService,
                          ILikeService likeService,
                          ICommentService commentService)
    {
        _postService = postService;
        _likeService = likeService;
        _commentService = commentService;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
            return Ok(await _postService.GetAll());

    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            return Ok(await _postService.Get(id));
        }
        catch (EntityCouldNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4222, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4000, ex.Message));
        }
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromForm] PostCreateDTO entity)
    {
        try
        {
            await _postService.Create(entity);
            return NoContent();
        }
        catch (EntityCouldNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4991, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4100, ex.Message));
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PostUpdateDTO entity)
    {
        try
        {
            await _postService.Update(id, entity);
            return NoContent();
        }
        catch (EntityCouldNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4991, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4100, ex.Message));
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _postService.Delete(id);
        return NoContent();
    }
    [HttpPost("Save")]
    public async Task<IActionResult> SavePost(PostSaveDTO postSaveDTO)
    {
        try
        {
            await _postService.PostSave(postSaveDTO);
            return NoContent();
        }
        catch (EntityCouldNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4991, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4100, ex.Message));
        }
    }
    [HttpGet("saved")]
    public async Task<ActionResult> Saved()
    {
        try
        {
           return Ok( await _postService.GetSavedPost());
          
        }
        catch (EntityCouldNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4991, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4100, ex.Message));
        }

    }


    [HttpPost("likeadd/{id}")]
    public async Task<ActionResult> LikeAddAsync(int id)
    {
        try
        {
            await _likeService.AddLikeAsync(id);
            return NoContent();
        }
        catch (EntityCouldNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4991, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4100, ex.Message));
        }

    }

    [HttpPut("likedelete/{id}")]
    public async Task<ActionResult> LikeDeleteAsync(int id)
    {
        try
        {
            await _likeService.DeleteLikeAsync(id);
            return NoContent();
        }
        catch (EntityCouldNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4991, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4100, ex.Message));
        }
    }
    [HttpPost("commentadd")]
    public async Task<ActionResult> CommentAddAsync(CommentCreateDTO commentCreateDto)
    {
        try
        {
            await _commentService.CreateCommentAsync(commentCreateDto);
            return NoContent();
        }
        catch (EntityCouldNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4991, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4100, ex.Message));
        }
    }

    [HttpPut("commentdelete/{id}")]
    public async Task<ActionResult> CommentDeleteAsync(int id)
    {
        try
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
        catch (EntityCouldNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4991, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response(4100, ex.Message));
        }
    }

}
