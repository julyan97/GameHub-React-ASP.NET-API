using Microsoft.AspNetCore.SignalR;

namespace GameHub.SignalR.Hubs
{
    public class NotificationHub : Hub
    {

        public async Task Notify()
        {
            await Clients.All.SendAsync("Recive");
        }
    }
}
