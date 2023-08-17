using GameHub.Common.Models.RequestModels.GameEvent;

namespace GameHub.Logic.Services.Player
{
    public interface IPayerService
    {
        Task ChangeStatusAsync(RequestChangePlayerStatus request);
    }
}