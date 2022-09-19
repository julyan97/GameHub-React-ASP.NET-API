using GameHub.BL.Services.IServices;
using GameHub.Common.Entities;
using GameHub.DAL.Repositories.Interfaces;

namespace GameHub.BL.Services
{
    public class PlayerService : BaseService, IPlayerService
    {

        public PlayerService(IRepository _repository) : base(_repository)
        {

        }

        public async Task<Player> AddAsync(Player player)
        {
            await repository.CreateAsync(player);
            return player;
        }

        public async Task<Player> ChangeStatusAsync(string name, bool status)
        {
            var player = repository
                .All<Player>()
                .FirstOrDefault(p => p.UsernameInGame == name);

            player.Status = status;

            return player;
        }

        public async Task<Player> DeleteAsync(string playerId)
        {
            var player = repository
                .All<Player>()
                .FirstOrDefault(p => p.Id == playerId);

            await repository.DeleteAsync(player);
            return player;
        }

        public Player FindById(string id)
        {
            var player = repository
                .AllReadOnly<Player>()
                .FirstOrDefault(p => p.Id == id);

            return player;
        }

        public Player FindPlayerByNick(string userNick)
        {
            return repository
                .AllReadOnly<Player>()
                .FirstOrDefault(x => x.UsernameInGame == userNick);
        }
    }
}
