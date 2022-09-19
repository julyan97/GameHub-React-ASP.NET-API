using GameHub.BL.Services.IServices;
using GameHub.Common.Entities;
using GameHub.DAL.Repositories.Interfaces;

namespace GameHub.BL.Services
{
    public class HomeService : BaseService, IHomeService
    {

        public HomeService(IRepository _repository) : base(_repository)
        {

        }

        public ICollection<Game> FindAllGames()
        {
            return repository.AllReadOnly<Game>()
                .ToList();

        }
    }
}
