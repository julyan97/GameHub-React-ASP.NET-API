using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.SignalR.Hubs.Clients
{
    public interface INotification
    {
        Task ReceiveMessage(string message);
        Task NotificationsUpdate();
        Task ReRenderDetails();
        Task ReRenderGameEventsPage();
    }
}
