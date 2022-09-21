using AutoMapper;
using GameHub.Common.Entities;
using GameHub.Common.Models.RequestModels;
using GameHub.Logic.Services.Event;
using GameHub.Logic.Services.Notification;
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

        public EventController(
            IMapper mapper,
            IEventService eventService,
            INotificationService notificationService)
        {
            _mapper = mapper;
            this.eventService=eventService;
            this.notificationService=notificationService;
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
                var tuple = await eventService.AddPlayerToEventAsync(eventId, playerName, userId);
                var notification = new Notification()
                {

                    Message = "Player " + playerName + " wants to join your event.",
                    SenderId = tuple.player.User.Id,
                    RecipientId = tuple.gameEvent.Owner.User.Id,
                    GameEvent = tuple.gameEvent,
                    IsRead = false

                };
                tuple.gameEvent.Owner.User.NotificationsRecived.Add(notification);
                await notificationService.SaveAsync();

                var notifications = notificationService.GetUserNotifications(tuple.gameEvent.Owner.User.Id);

                await notificationService.Send(tuple.gameEvent.Owner.User.UserName, new
                {
                    Notifications = notifications.OrderByDescending(x => x.CreatedAt).ToArray(),
                    NotCount = tuple.gameEvent.Owner.User.NotificationsRecived.Count(n => n.IsRead == false)
                });
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }

            return Ok("Successfuly added player");
        }
    }
}
