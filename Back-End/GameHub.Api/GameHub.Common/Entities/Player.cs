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
    public class Player : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        public bool Status { get; set; }

        [Required]
        [MinLength(1), MaxLength(20)]
        public string UsernameInGame { get; set; }

        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<GameEvent> GameEventsOwn { get; set; }

        [JsonIgnore]
        public virtual ICollection<GameEvent> GameEventsParticipates { get; set; }

        public Player()
        {
            GameEventsOwn = new List<GameEvent>();
            GameEventsParticipates = new List<GameEvent>();
        }

    }
}
