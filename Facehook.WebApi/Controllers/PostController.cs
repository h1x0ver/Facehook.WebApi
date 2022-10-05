using AutoMapper;
using Facehook.Business.Services;
using Facehook.Entity.DTO.Post;
using Facehook.Exceptions.EntityExceptions;
using Facehook.WebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Facehook.WebApi.Controllers;

[Route("api/[controller]"), ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
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
            var data = await _postService.Get(id);
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
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromForm] PostCreateDTO entity)
    {
        //try
        //{
            await _postService.Create(entity);
            return NoContent();
        //}
        //catch (EntityCouldNotFoundException ex)
        //{

        //    return StatusCode(StatusCodes.Status404NotFound, new Response(4991, ex.Message));

        //}
        //catch (Exception ex)
        //{
        //    return StatusCode(StatusCodes.Status404NotFound, new Response(4100, ex.Message));
        //}
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
}
