using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameHub.Common.Entities
{
    public class GameEvent : BaseEntity
    {
        public string OwnerId { get; set; }

        public string GameId { get; set; }

        public string DiscordUrl { get; set; }

        public string Description { get; set; }

        public string Rank { get; set; }

        [Required]
        public int NumberOfPlayers { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }

        [ForeignKey(nameof(GameId))]
        public virtual Game Game { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual Player Owner { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public GameEvent()
        {
            Players = new List<Player>();
        }
    }
}
