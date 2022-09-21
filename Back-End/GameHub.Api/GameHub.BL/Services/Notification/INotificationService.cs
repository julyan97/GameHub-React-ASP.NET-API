namespace GameHub.Logic.Services.Notification
{
    public interface INotificationService
    {
        Task Send(string recepientName, object param);
        IEnumerable<Common.Entities.Notification> GetUserNotifications(string userId);
        Task SaveAsync();
    }
}