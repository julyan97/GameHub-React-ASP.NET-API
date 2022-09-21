using GameHub.DAL.Repositories.Interfaces;
using GameHub.Common.Entities;

namespace GameHub.Logic.Services.Game
{
    public class GameService : IGameService
    {
        private readonly IRepository repository;

        public GameService(IRepository repository)
        {
            this.repository=repository;
        }

        public Common.Entities.Game? GetByName(string name)
        {
            return repository
                .All<Common.Entities.Game>(x => x.GameName == name)
                .FirstOrDefault();
        }

    }
}
