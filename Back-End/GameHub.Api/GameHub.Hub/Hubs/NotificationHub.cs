using GameHub.Common.Entities;
using GameHub.DAL.Repositories.Interfaces;
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
        private readonly IRepository repository;

        public NotificationHub(IRepository repository)
        {
            this.repository=repository;
        }

        public async Task SendNotificationToAll()
        {
            await Clients.All.NotificationsUpdate();
        }
        public async Task AddToGroup(string groupId, string userName)
        {
            var userConnectionId = UserConnectionIdentities[userName].UserIndentifier;
            await Groups.AddToGroupAsync(userConnectionId, groupId);
        }

        public async Task SendNotificationToGroup(string groupId)
        {
            await Clients.Group(groupId).NotificationsUpdate();
        }

        public async Task SendNotificationToUser(string userId)
        {
            var userName = repository.AllReadOnly<User>(x => x.Id == userId)
                .FirstOrDefault()?
                .Email;
            var userIdentifier = UserConnectionIdentities[userName].UserIndentifier;
            await Clients.User(userIdentifier).NotificationsUpdate();
        }

        public async Task UpdateAllNotificationDetails()
        {
            await Clients.All.ReRenderDetails();
        }

        public async Task UpdateAllGameEventsPages()
        {
            await Clients.All.ReRenderGameEventsPage();
        }
        public override async Task OnConnectedAsync()
        {
            var userName = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userConnectionId = Context.ConnectionId;
            var userIdentifier = Context.UserIdentifier;


            if (userName != null)
            {
                UserConnectionIdentities[userName] = new() 
                {
                    UserConnection = userConnectionId,
                    UserIndentifier = userIdentifier
                };
            }

             await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

    }
}
