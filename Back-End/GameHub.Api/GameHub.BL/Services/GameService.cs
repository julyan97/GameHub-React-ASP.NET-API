using GameHub.BL.Services.IServices;
using GameHub.Common.Entities;
using GameHub.DAL.Repositories.Interfaces;

namespace GameHub.BL.Services
{
    public class GameService : BaseService, IGameService
    {

        public GameService(IRepository _repository) : base(_repository)
        {
        }

        public async Task AddAsync(Game game)
        {
            await repository.CreateAsync(game);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var game = FindGameById(id);
            var events = repository.AllReadOnly<GameEvent>()
                .Where(x => x.Game.Id == game.Id)
                .ToList();

            await repository.DeleteRangeAsync(events);
            await repository.DeleteAsync(game);
            await repository.SaveChangesAsync();
        }

        public List<Game> FindAll()
        {
            return repository.AllReadOnly<Game>()
                .ToList();
        }

        public Game FindGameById(string id)
        {
            return repository.AllReadOnly<Game>()
                .FirstOrDefault(g => g.Id == id);
        }

        public Game FindGameByName(string name)
        {
            return repository.AllReadOnly<Game>()
                .FirstOrDefault(x => x.GameName == name);
        }
    }
}
