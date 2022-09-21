using AutoMapper;
using GameHub.Common.Entities;
using GameHub.Common.Models.RequestModels;
using GameHub.Logic.Services.Event;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        public readonly IMapper _mapper;
        private readonly IEventService eventService;

        public EventController(
            IMapper mapper,
            IEventService eventService)
        {
            _mapper = mapper;
            this.eventService=eventService;
        }

        [HttpPost("Events")]
        public async Task<IActionResult> Events()
        {
            return Ok(eventService.GetAll().ToList());
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody]RequestCreateEvent request)
        {
            var userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var res = await eventService.GenerateEventAsync(request, userName);
            return Ok(res);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            await eventService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("AddPlayerToEvent")]
        public async Task<IActionResult> AddPlayerToEvent(string eventId, string playerName)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            try
            {
                await eventService.AddPlayerToEventAsync(eventId, playerName, userId);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }

            return Ok("Successfuly added player");
        }
    }
}
