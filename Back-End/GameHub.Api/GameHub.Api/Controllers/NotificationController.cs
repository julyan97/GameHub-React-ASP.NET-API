using GameHub.Logic.Services.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService=notificationService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllOfAuthUser()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            var notifications = notificationService.GetNotificationsByUserId(userId);

            return Ok(notifications.OrderByDescending(x => x.CreatedAt));
        }

        [HttpGet("SetToRead/{NotificationId}")]
        public IActionResult NotificationRead(string NotificationId)
        {
            notificationService.SetNotificationAsync(NotificationId, true);
            return Ok();
        }
    }
}
