using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.Common.Models.RequestModels.GameEvent
{
    public class RequestCreateEvent
    {
        public string OwnerInGameName { get; set; }
        public string Description { get; set; }
        public int NumberOfPlayers { get; set; }
        public string Rank { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        public string GameName { get; set; }
        public string DiscordUrl { get; set; }
    }
}
