using GameHub.Common.Entities;
using GameHub.DAL.Data.Extensions;
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
    public class GameHubDbContext : IdentityDbContext<User>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameEvent> GameEvents { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public GameHubDbContext(DbContextOptions<GameHubDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.BuildPlayer();
            builder.BuildCategory();
            builder.BuildUser();
            builder.BuildNotification();
            builder.Seed(); 
            base.OnModelCreating(builder);
        }

    }
}
