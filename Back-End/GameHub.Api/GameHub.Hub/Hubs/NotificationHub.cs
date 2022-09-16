using GameHub.SignalR.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;

namespace GameHub.SignalR.Hubs
{
    public class NotificationHub : Hub<INotification>
    {

        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage(message);
        }

        public async Task SendNotification()
        {
            await Clients.All.NotificationsUpdate();
        }
    }
}
