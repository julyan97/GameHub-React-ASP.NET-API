using GameHub.Common.Entities;

namespace GameHub.BL.Services.IServices
{
    public interface IGameService : IBaseService
    {
        public Game FindGameByName(string name);
        public Game FindGameById(string id);
        public Task AddAsync(Game game);
        public Task DeleteAsync(string id);
        public List<Game> FindAll();

    }
}
