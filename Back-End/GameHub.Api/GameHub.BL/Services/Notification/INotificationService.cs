namespace GameHub.Logic.Services.Notification
{
    public interface INotificationService
    {
        Task Send(string recepientName, object param);
        IEnumerable<Common.Entities.Notification> GetNotificationsByUserId(string userId);
        Task SetNotificationAsync(string NotificattionId, bool isRead);
        Task SaveAsync();
    }
}