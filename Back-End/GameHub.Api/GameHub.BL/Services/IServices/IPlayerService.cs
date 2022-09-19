using GameHub.Common.Entities;

namespace GameHub.BL.Services.IServices
{
    public interface IPlayerService : IBaseService
    {
        public Task<Player> DeleteAsync(string playerId);
        public Player FindById(string id);
        public Task<Player> AddAsync(Player player);
        public Player FindPlayerByNick(string userNick);
        public Task<Player> ChangeStatusAsync(string name, bool status);
    }
}