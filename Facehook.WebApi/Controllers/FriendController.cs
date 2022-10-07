using Facehook.Business.DTO_s.User;
using Facehook.Business.Services;
using Facehook.Exceptions.EntityExceptions;
using Facehook.WebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facehook.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class FriendController : Controller
{
    private readonly IUserFriendService _friendService;

    public FriendController(IUserFriendService friendService)
    {
        _friendService = friendService;
    }

    [HttpPost("addFriend/{id}")]
    public async Task<ActionResult> AddFriendAsync(string? id)
    {
        try
        {
            await _friendService.FriendAddAsync(id);
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

    [HttpPost("acceptFriend/{id}")]
    public async Task<ActionResult> AcceptFriendAsync(string? id)
    {
        try
        {
            await _friendService.FriendAcceptAsync(id);
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

    [HttpPut("deleteFriend/{id}")]
    public async Task<ActionResult> RemoveFriendAsync(string? id)
    {
        try
        {
            await  _friendService.FriendRemoveAsync(id);
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

    [HttpPut("rejectFriend/{id}")]
    public async Task<ActionResult> RejectFriendAsync(string? id)
    {
        try
        {
            await _friendService.FriendRejectAsync(id);
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

    [HttpGet("friendRequests")]
    public async Task<ActionResult> FriendRequestAsync()
    {
        try
        {
            return Ok(await _friendService.GetFriendRequestAsync());
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

    [HttpGet("friendSuggestion")]
    public async Task<ActionResult> GetFriendSuggestionAsync()
    {
        try
        {
            return Ok(await _friendService.GetFriendSuggestionAsync());
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

    [HttpGet("getAllFriend")]
    public async Task<ActionResult<List<UserGetDTO>>> GetAllFriendAsync()
    {
        try
        {
            return Ok(await _friendService.FriendGetAllAsync());
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



