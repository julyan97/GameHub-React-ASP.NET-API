using GameHub.Common.Entities;
using GameHub.Common.Models.RequestModels;

namespace GameHub.Logic.Services.Event
{
    public interface IEventService
    {
        Task<GameEvent> GenerateEventAsync(RequestEvent gameEvent, string UserIdentity);
        IEnumerable<GameEvent> GetAll();
        Task DeleteAsync(string id);
    }
}