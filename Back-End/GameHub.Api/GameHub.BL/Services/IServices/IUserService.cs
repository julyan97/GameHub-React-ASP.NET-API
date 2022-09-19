using GameHub.Common.Entities;

namespace GameHub.BL.Services.IServices
{
    public interface IUserService : IBaseService
    {
        public User FindUserById(string userId);
        public User FindUserByName(string userName);
        public List<User> FindAll();
        public Task DeleteAsync(string id);
        public Task<List<Notification>> ChangeNotificationStatusToReadAsync(string userId);
        public List<Notification> FindAllNotificationsByUserId(string userId);
        public List<Notification> FindAllNotificationsByUserName(string name);
        public Task<Notification> AddNotificationAsync(Notification notification, string userId);
    }
}