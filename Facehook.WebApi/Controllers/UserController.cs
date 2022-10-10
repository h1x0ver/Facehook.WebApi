using Facehook.Business.DTO_s.User;
using Facehook.Business.Services;
using Facehook.Exceptions.EntityExceptions;
using Facehook.WebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facehook.WebApi.Controllers
{
    [Route("api/[controller]"),Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var user = await _userService.Get(id);
                return Ok(user);
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var user = await _userService.GetAll();
                return Ok(user);
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
        [HttpPost("userUpdate")]
        public async Task<ActionResult> UserUpdateAsyncs(UserUpdateDTO entity)
        {
            try
            {
                await _userService.Update(entity);
                return NoContent();
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
        [HttpPost("changeProfilePhoto")]
        public async Task<ActionResult> ChangeProfilePhoto([FromForm] ProfilePhotoDTO profilePhotoDto)
        {
            try
            {
                await _userService.ChangeProfilePhotoAsync(profilePhotoDto);
                return NoContent();
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
        [HttpGet("userProfile/{id}")]
        public async Task<ActionResult> UserProfileAsync(string? id)
        {
            try
            {
                return Ok(await _userService.GetUserProfileAsync(id));
            }
            catch (EntityCouldNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response(4222, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response(4000, ex.StackTrace));
            }
        }

    }
}