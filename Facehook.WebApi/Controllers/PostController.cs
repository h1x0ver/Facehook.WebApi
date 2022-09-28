using AutoMapper;
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
    private readonly IMapper _mapper;

    public PostController(IPostService postService, IMapper mapper)
    {
        _postService = postService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var data = await _postService.GetAll();
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
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var data =  await _postService.Get(id);
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
    [HttpPost("create") ]
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

}
