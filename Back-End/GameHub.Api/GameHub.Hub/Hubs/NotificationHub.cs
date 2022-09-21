using GameHub.SignalR.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace GameHub.SignalR.Hubs
{
    public class UserConnectionIndentity
    {
        public string? UserConnection { get; set; }
        public string? UserIndentifier { get; set; }
    }

    public class NotificationHub : Hub<INotification>
    {
        public static Dictionary<string?, UserConnectionIndentity> UserConnectionIdentities = new();

        public async Task SendNotificationToAll()
        {
            await Clients.All.NotificationsUpdate();
        }
        public async Task AddToGroup(string groupName, string userName)
        {
            var userConnectionId = UserConnectionIdentities[userName].UserConnection;
            await Groups.AddToGroupAsync(userConnectionId, groupName);
        }

        public async Task SendNotificationToGroup(string groupName)
        {
            await Clients.Group(groupName).NotificationsUpdate();
        }

        public override Task OnConnectedAsync()
        {
            var userName = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userConnectionId = Context.ConnectionId;
            var userIdentifier = Context.UserIdentifier;

            if (userName != null)
            {
                UserConnectionIdentities.Add(userName, new() 
                {
                    UserConnection = userConnectionId,
                    UserIndentifier = userIdentifier
                });
            }

            return base.OnConnectedAsync();
        }
    }
}
