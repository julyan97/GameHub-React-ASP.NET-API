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
    public class Notification : BaseEntity, IEquatable<Notification>
    {
        public string GameEventId { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string RecipientId { get; set; }

        [Required]
        public string Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(GameEventId))]
        public virtual GameEvent GameEvent { get; set; }

        [ForeignKey(nameof(SenderId))]
        [JsonIgnore]
        public virtual User Sender { get; set; }

        [ForeignKey(nameof(RecipientId))]
        [JsonIgnore]
        public virtual User Recipient { get; set; }

        public Notification()
        {
            CreatedAt = DateTime.Now;
        }

        public bool Equals(Notification other)
        {
            return SenderId.Equals(other.SenderId) &&
                   RecipientId.Equals(other.RecipientId) &&
                   Message.Equals(other.Message);
        }
    }
}
