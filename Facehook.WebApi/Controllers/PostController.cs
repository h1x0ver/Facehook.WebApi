using Facehook.Business.Services;
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
            return Ok(data);

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


}
