using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.Common.Entities
{
    public class Category : BaseEntity
    {
        public string Type { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public Category()
        {
            Posts = new List<Post>();
        }
    }
}
