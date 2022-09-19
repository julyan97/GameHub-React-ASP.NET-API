using GameHub.BL.Services.IServices;
using GameHub.Common.Entities;
using GameHub.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameHub.BL.Services
{
    public class UserService : BaseService, IUserService
    {

        public UserService(IRepository _repository) : base(_repository)
        {

        }

        public async Task<Notification> AddNotificationAsync(Notification notification, string userId)
        {
            var user = await repository
                .All<User>(u => u.Id == userId)
                .Include(x => x.NotificationsRecived)
                .FirstOrDefaultAsync();

            if (!user.NotificationsRecived.Contains(notification))
            {
                user.NotificationsRecived.Add(notification);
                await repository.SaveChangesAsync();
            }

            return notification;
        }

        public async Task<List<Notification>> ChangeNotificationStatusToReadAsync(string userId)
        {
            var notifications = repository.All<Notification>(n => n.RecipientId == userId);

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await repository.SaveChangesAsync();
            return notifications.ToList();
        }

        public async Task DeleteAsync(string id)
        {
            var user = repository
                .All<User>(x => x.Id == id)
                .FirstOrDefault();

            await repository.DeleteAsync(user);
        }

        public List<User> FindAll()
        {
            return repository.AllReadOnly<User>()
                .ToList();
        }

        public List<Notification> FindAllNotificationsByUserId(string id)
        {
            return repository.AllReadOnly<User>()
                .Include(x => x.NotificationsRecived)
                .Where(x => x.Id == id)
                .Select(x => x.NotificationsRecived
                             .OrderBy(x => x.CreatedAt))
                .FirstOrDefault()
                .ToList();
        }

        public List<Notification> FindAllNotificationsByUserName(string name)
        {
            return repository.AllReadOnly<User>()
                .Include(x => x.NotificationsRecived)
                .Where(x => x.UserName == name)
                .Select(x => x.NotificationsRecived
                             .OrderBy(x => x.CreatedAt))
                .FirstOrDefault()
                .ToList();
        }

        public User FindUserById(string userId)
        {
            return repository.AllReadOnly<User>(u => u.Id == userId)
                .FirstOrDefault();
        }

        public User FindUserByName(string userName)
        {
            return repository.AllReadOnly<User>(u => u.UserName == userName)
                .FirstOrDefault();
        }

    }
}