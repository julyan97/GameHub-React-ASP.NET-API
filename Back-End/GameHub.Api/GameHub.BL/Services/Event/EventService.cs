using AutoMapper;
using GameHub.Common.Entities;
using GameHub.Common.Models.RequestModels.GameEvent;
using GameHub.DAL.Repositories.Interfaces;
using GameHub.Logic.Services.Game;
using GameHub.Logic.Services.User;
using GameHub.SignalR.Hubs;
using Microsoft.EntityFrameworkCore;

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

        public async Task<GameEvent> GenerateEventAsync(RequestCreateEvent gameEvent, string userName)
        {
            var gameEventMap = mapper.Map<GameEvent>(gameEvent);
            var game = gameService.GetByName(gameEvent.GameName);
            gameEventMap.Game = game;

            var player = await repository.CreateAsync(new Common.Entities.Player()
            {
                User = userService.GetByName(userName),
                UsernameInGame = gameEvent.OwnerInGameName
            });

            gameEventMap.OwnerId = player.Id;

            gameEventMap.Players.Add(player);
            var res = await repository.CreateAsync(gameEventMap);

            await repository.SaveChangesAsync();
            return res;

        }

        public IEnumerable<GameEvent> GetAll()
        {
            return repository.All<GameEvent>()
                .Include(x => x.Players)
                .Include(x => x.Owner)
                .Include(x => x.Game);
        }

        public async Task<GameEvent?> GetByIdAsync(string id)
        {
            return await repository.All<GameEvent>()
                .Include(x =>x.Players)
                .ThenInclude(x => x.User)
                .Include(x => x.Owner)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.NotificationsRecived)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Common.Entities.Player? ContainPlayer(string eventId, string playerName)
        {
            var res = repository.All<GameEvent>()
                .Include(x => x.Players)
                .FirstOrDefault(x => x.Id == eventId)?
                .Players
                .FirstOrDefault(x => x.UsernameInGame == playerName); ;

            return res;
        }

        public async Task<(GameEvent gameEvent, Common.Entities.Player player)> AddPlayerToEventAsync(string eventId, string playerName, string playerUserId)
        {
            var gameEvent = await GetByIdAsync(eventId);
            var player = ContainPlayer(eventId, playerName);

            if (gameEvent == null || player != null)
                throw new Exception("event may not exist, or the player has already joined the event");

            var newPlayer = new Common.Entities.Player
            {
                User = userService.GetById(playerUserId),
                UsernameInGame = playerName
            };

            gameEvent.Players.Add(newPlayer);
            await repository.SaveChangesAsync();

            return (gameEvent, newPlayer);

        }

        public async Task RemovePlayerFromEventByNameAsync(RequestAddPlayerToEvent parameters)
        {
            var players = (await GetByIdAsync(parameters.EventId))?
                .Players;

            if(players != null)
            {
                var player = players.FirstOrDefault(x => x.UsernameInGame == parameters.PlayerName);
                players.Remove(player);

                
            }

            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = repository.All<GameEvent>(x => x.Id == id).FirstOrDefault();
            await repository.DeleteAsync<GameEvent>(entity);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAllExpiredGameEventsAsync()
        {
            var expiredEvents = repository.AllReadOnly<GameEvent>()
                .Where(g => g.DueDate <= DateTime.Now);
            await repository.DeleteRangeAsync(expiredEvents);
        }

    }
}
