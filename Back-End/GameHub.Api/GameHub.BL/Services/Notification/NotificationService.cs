using GameHub.DAL.Repositories.Interfaces;
using GameHub.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.Logic.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository repository;
        private readonly IHubContext<NotificationHub> hub;

        public NotificationService(IRepository repository, IHubContext<NotificationHub> hub)
        {
            this.repository=repository;
            this.hub=hub;
        }

        public async Task Send(string recepientName, object param)
        {
            await hub.Clients
                .User(NotificationHub.UserConnectionIdentities[recepientName].UserIndentifier)
                .SendAsync("NotificationsUpdate", param);
        }

        public IEnumerable<Common.Entities.Notification> GetNotificationsByUserId(string userId)
        {
            return repository.All<Common.Entities.User>()
                .Include(x => x.NotificationsRecived)
                .FirstOrDefault(x => x.Id == userId)
                .NotificationsRecived;
        }

        public async Task SetNotificationAsync(string NotificattionId, bool isRead)
        {
            var notification = repository.All<Common.Entities.Notification>()
                .FirstOrDefault(x => x.Id == NotificattionId);
            notification.IsRead = isRead;

            await repository.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await repository.SaveChangesAsync();
        }
    }
}
