using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.Common.Models.RequestModels.GameEvent
{
    public class RequestChangePlayerStatus
    {
        public string EventId { get; set; }
        public string PlayerName { get; set; }
        public bool Status { get; set; }
    }
}
