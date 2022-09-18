using GameHub.SignalR.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace GameHub.SignalR.Hubs
{
    public class NotificationHub : Hub<INotification>
    {
        public static Dictionary<string, string> UserConnectionIds = new();

        public async Task SendNotificationToAll()
        {
            await Clients.All.NotificationsUpdate();
        }
        public async Task AddToGroup(string groupName, string userName)
        {
            var userConnectionId = UserConnectionIds[userName];
            await Groups.AddToGroupAsync(userConnectionId, groupName);
        }

        public async Task SendNotificationToGroup(string groupName)
        {
            await Clients.Group(groupName).NotificationsUpdate();
        }

        public override Task OnConnectedAsync()
        {
            var userName = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userConnectioniId = Context.ConnectionId;

            if (userName != null)
            {
                UserConnectionIds.Add(userName, userConnectioniId);
            }

            return base.OnConnectedAsync();
        }
    }
}
