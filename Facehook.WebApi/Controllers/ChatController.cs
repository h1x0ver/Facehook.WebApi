using Facehook.Business.DTO_s.Message;
using Facehook.Business.Services;
using Facehook.Exceptions.EntityExceptions;
using Facehook.WebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facehook.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IMessageService _messageService;

    public ChatController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost]
    public async Task<ActionResult<MessageGetDTO>> SendMessage(MessageDTO message)
    {
        try
        {
            return Ok(await _messageService.SendMessage(message));
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
    [HttpGet("{username}")]
    public async Task<ActionResult<List<MessageDTO>>> GetMessages(string username)
    {
        try
        {
            return Ok(await _messageService.GetMessages(username));
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