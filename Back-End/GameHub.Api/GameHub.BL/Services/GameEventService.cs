using GameHub.BL.Services.IServices;
using GameHub.Common.Models;
using GameHub.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.BL.Services
{
    public class GameEventService : BaseService, IGameEventService
    {
        public GameEventService(IRepository _repository) : base(_repository)
        {
        }

        public List<GameEvent> FindEventsByGameName(string gameName, int? pageNumber, int? pageSize)
        {
            return repository.AllReadOnly<GameEvent>()
                 .Include(x => x.Game)
                 .Where(x => x.Game.GameName == gameName)
                 .OrderBy(x => x.StartDate)
                 .Skip((pageNumber ?? 1 - 1) * pageSize ?? 1)
                 .Take(pageSize ?? 1)
                 .ToList();
        }

        public GameEvent FindEventById(Guid id)
        {
            return repository.AllReadOnly<GameEvent>()
                 .FirstOrDefault(x => x.Id == id.ToString());
        }

        public async Task AddAsync(GameEvent gameEvent)
        {
            await repository.CreateAsync(gameEvent);
        }

        public List<GameEvent> FindAll(int? pageNumber, int? pageSize)
        {
            return repository.AllReadOnly<GameEvent>()
                .OrderBy(x => x.StartDate)
                .Skip((pageNumber ?? 1 - 1) * pageSize ?? 1)
                .Take(pageSize ?? 1)
                .ToList();
        }

        public List<GameEvent> FindAll()
        {
            return repository.AllReadOnly<GameEvent>()
                .ToList();
        }

        public async Task DeleteEventAsync(Guid id)
        {
            var gameEvent = await repository.AllReadOnly<GameEvent>()
                  .FirstOrDefaultAsync(x => x.Id == id.ToString());
            await repository.DeleteAsync(gameEvent);
        }

        public async Task AddPlayerAsync(Player player, Guid gameEventId)
        {
            var gameEvent = await repository.All<GameEvent>()
                .Include(x => x.Players)
                .FirstOrDefaultAsync(x => x.Id == gameEventId.ToString());
            if (!gameEvent.Players.Contains(player))
            {
                gameEvent.Players.Add(player);
            }
        }

        public Player FindPlayerByNick(string userNick, Guid gameEventId)
        {
            return repository.AllReadOnly<GameEvent>()
                .Include(x => x.Players)
                .Where(x => x.Id == gameEventId.ToString())
                .Select(x => x.Players.FirstOrDefault(y => y.UsernameInGame.Equals(userNick)))
                .FirstOrDefault();
        }

        public async Task DeleteAllExpiredGameEventsAsync()
        {
            var expiredEvents = repository.AllReadOnly<GameEvent>()
                .Where(g => g.DueDate <= DateTime.Now);

            await repository.DeleteRangeAsync(expiredEvents);
        }

    }
}
