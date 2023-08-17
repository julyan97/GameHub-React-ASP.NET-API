using GameHub.Common.Entities;
using GameHub.Common.Models.RequestModels.GameEvent;

namespace GameHub.Logic.Services.Event
{
    public interface IEventService
    {
        Task<GameEvent> GenerateEventAsync(RequestCreateEvent gameEvent, string UserIdentity);
        Task<(GameEvent gameEvent, Common.Entities.Player player)> AddPlayerToEventAsync(string eventId, string playerName, string playerUserId);
        IEnumerable<GameEvent> GetAll();
        Task<GameEvent> GetByIdAsync(string id);
        Task RemovePlayerFromEventByNameAsync(RequestAddPlayerToEvent parameters);
        Task DeleteAsync(string id);
        Task DeleteAllExpiredGameEventsAsync();
        Common.Entities.Player? ContainPlayer(string eventId, string playerName);
    }
}