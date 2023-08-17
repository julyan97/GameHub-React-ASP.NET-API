using GameHub.Common.Entities;
using GameHub.Common.Models.RequestModels.GameEvent;
using GameHub.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.Logic.Services.Player
{
    public class PayerService : IPayerService
    {
        private readonly IRepository repository;

        public PayerService(IRepository repository)
        {
            this.repository=repository;
        }

        public async Task ChangeStatusAsync(RequestChangePlayerStatus parameters)
        {
            var player = repository.All<GameEvent>(x => x.Id == parameters.EventId)
                .Include(x => x.Players)
                .FirstOrDefault()?
                .Players
                .FirstOrDefault(x => x.UsernameInGame == parameters.PlayerName);

            player.Status = parameters.Status;

            await repository.SaveChangesAsync();
        }

    }
}
