using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.Common.Entities
{
    public class Game : BaseEntity
    {
        [Required]
        [MinLength(1), MaxLength(20)]
        public string GameName { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }
    }
}
