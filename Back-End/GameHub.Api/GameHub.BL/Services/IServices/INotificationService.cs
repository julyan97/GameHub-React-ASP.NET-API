using GameHub.Common.Entities;

namespace GameHub.BL.Services.IServices
{
    public interface INotificationService : IBaseService
    {
        public Task<Notification> AddAsync(Notification notification);
        public Task<Notification> DeleteAsync(Notification notification);
        public List<Notification> GetForEvent(GameEvent gameEvent);

    }
}
