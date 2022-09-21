using GameHub.Common.Entities;
using GameHub.Common.Models.RequestModels;

namespace GameHub.Logic.Services.Event
{
    public interface IEventService
    {
        Task<GameEvent> GenerateEventAsync(RequestCreateEvent gameEvent, string UserIdentity);
        Task<GameEvent> AddPlayerToEventAsync(string eventId, string playerName, string playerUserId);
        IEnumerable<GameEvent> GetAll();
        Task<GameEvent> GetById(string id);
        Task DeleteAsync(string id);
        Task DeleteAllExpiredGameEventsAsync();
        Common.Entities.Player? ContainPlayer(string eventId, string playerName);
    }
}