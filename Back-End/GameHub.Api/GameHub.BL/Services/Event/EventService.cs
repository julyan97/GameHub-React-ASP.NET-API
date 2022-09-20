using AutoMapper;
using GameHub.Common.Entities;
using GameHub.Common.Models.RequestModels;
using GameHub.DAL.Repositories.Interfaces;
using GameHub.Logic.Services.Game;
using GameHub.Logic.Services.User;

namespace GameHub.Logic.Services.Event
{
    public class EventService : IEventService
    {
        private readonly IRepository repository;
        private readonly IGameService gameService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public EventService(
            IRepository repository,
            IGameService gameService,
            IUserService userService,
            IMapper map)
        {
            this.repository=repository;
            this.gameService=gameService;
            this.userService=userService;
            mapper=map;
        }

        public async Task<GameEvent> GenerateEventAsync(RequestEvent gameEvent, string userName)
        {
            var gameEve = mapper.Map<GameEvent>(gameEvent);
            var game = gameService.GetByName(gameEvent.GameName);
            gameEve.Game = game;

            var player = await repository.CreateAsync(new Common.Entities.Player()
            {
                User = userService.GetByName(userName),
                UsernameInGame = gameEvent.OwnerInGameName
            });

            gameEve.OwnerId = player.Id;

            player.GameEventsOwn.Add(gameEve);
            var res = await repository.CreateAsync(gameEve);

            await repository.SaveChangesAsync();
            return res;

        }

        public IEnumerable<GameEvent> GetAll()
        {
            return repository.AllReadOnly<GameEvent>();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = repository.All<GameEvent>(x => x.Id == id).FirstOrDefault();
            await repository.DeleteAsync<GameEvent>(entity);
            await repository.SaveChangesAsync();
        }

    }
}
