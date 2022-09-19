using Facehook.Business.Services;
using Facehook.Entity.DTO.Post;
using Facehook.Entity.Entites;
using Facehook.Exceptions.EntityExceptions;
using Facehook.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
namespace Facehook.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    public IPostService PostService => _postService;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var data = await PostService.GetAll();
            List<PostGetDto> dtos = new List<PostGetDto>();
            foreach (var item in data)
            {
                dtos.Add(MapGetDto(item));
            }
            return Ok(dtos);

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

    private PostGetDto MapGetDto(Post post)
    {
        var data = new PostGetDto();
        data.Id = post.Id;
        data.ImageName = post.PostImages?.FirstOrDefault()?.Image?.Name;
        data.Title = post.Title;
        return data;
    }


}
