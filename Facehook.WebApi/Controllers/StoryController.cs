using Facehook.Business.DTO_s.Story;
using Facehook.Business.Services;
using Facehook.Exceptions.EntityExceptions;
using Facehook.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Facehook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;

        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _storyService.GetAll();
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
                var data = await _storyService.Get(id);
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
        public async Task<IActionResult> CreateAsync([FromForm] StoryCreateDTO entity)
        {
            try
            {
                await _storyService.Create(entity);
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
            await _storyService.Delete(id);
            return NoContent();
        }
    }

}
