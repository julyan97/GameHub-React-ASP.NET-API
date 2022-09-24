using AutoMapper;
using GameHub.Common.Entities;
using GameHub.Common.Models.RequestModels.GameEvent;
using GameHub.Logic.Services.Event;
using GameHub.Logic.Services.Notification;
using GameHub.Logic.Services.Player;
using GameHub.SignalR.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Claims;

namespace GameHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        public readonly IMapper _mapper;
        private readonly IEventService eventService;
        private readonly INotificationService notificationService;
        private readonly IPayerService payerService;

        public EventController(
            IMapper mapper,
            IEventService eventService,
            INotificationService notificationService,
            IPayerService payerService)
        {
            _mapper = mapper;
            this.eventService=eventService;
            this.notificationService=notificationService;
            this.payerService=payerService;
        }

        [HttpGet("Events")]
        public async Task<IActionResult> Events()
        {
            var events = eventService.GetAll().ToList();
            return Ok(events);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var GameEvent = await eventService.GetByIdAsync(id);
            return Ok(GameEvent);
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody]RequestCreateEvent request)
        {
            try
            {
                var userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                var res = await eventService.GenerateEventAsync(request, userName);
                return Ok(res);
            }
            catch(Exception e)
            {
                return BadRequest("Cant Sry:"+ e);
            }
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            await eventService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("AddPlayerToEvent")]
        public async Task<IActionResult> AddPlayerToEvent(RequestAddPlayerToEvent request)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            try
            {
                var tuple = await eventService.AddPlayerToEventAsync(request.EventId, request.PlayerName, userId);

                var notification = new Notification()
                {

                    Message = "Player " + request.PlayerName + " wants to join your event.",
                    SenderId = tuple.player.User.Id,
                    RecipientId = tuple.gameEvent.OwnerId,
                    GameEvent = tuple.gameEvent,
                    IsRead = false

                };
                tuple.gameEvent.Owner.User.NotificationsRecived.Add(notification);
                await notificationService.SaveAsync();

               // var notifications = notificationService.GetUserNotifications(tuple.gameEvent.Owner.User.Id);

            }
            catch(Exception e)
            {
                return BadRequest(e);
            }

            return Ok("Successfuly added player");
        }

        [HttpPost("RemovePlayerToEvent")]
        public async Task<IActionResult> RemovePlayerToEvent(RequestAddPlayerToEvent request)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            try
            {
               await eventService.RemovePlayerFromEventByNameAsync(request);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok("Successfuly added player");
        }

        [HttpPost("ChangePlayerStatus")]
        public async Task<IActionResult> RemovePlayerToEvent(RequestChangePlayerStatus request)
        {
            try
            {
                await payerService.ChangeStatusAsync(request);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok("Successfuly changed Status");
        }
    }
}
