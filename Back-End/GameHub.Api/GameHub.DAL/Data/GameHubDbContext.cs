using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.DAL.Data
{
    public class GameHubDbContext : IdentityDbContext<IdentityUser>
    {
        public GameHubDbContext(DbContextOptions<GameHubDbContext> options) : base(options)
        {

        }
    }
}
