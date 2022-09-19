using GameHub.Common.Entities;

namespace GameHub.BL.Services.IServices
{
    public interface IHomeService : IBaseService
    {
        public ICollection<Game> FindAllGames();
    }
}