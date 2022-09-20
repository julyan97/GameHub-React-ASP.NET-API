using GameHub.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.DAL.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void BuildPlayer(this ModelBuilder builder)
        {
            builder.Entity<Player>(player =>
            {
                player.HasMany(x => x.GameEventsOwn)
                .WithOne(y => y.Owner)
                .OnDelete(DeleteBehavior.NoAction);

                player.HasMany(x => x.GameEventsParticipates)
               .WithMany(y => y.Players);
            });
        }

        public static void BuildCategory(this ModelBuilder builder)
        {
            builder.Entity<Category>(category =>
            {
                category.HasMany(x => x.Posts)
                .WithOne(y => y.Category)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public static void BuildUser(this ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user.HasMany(x => x.NotificationsRecived)
                .WithOne(y => y.Recipient)
                .OnDelete(DeleteBehavior.Cascade);

                user.HasMany(x => x.NotificationsSend)
               .WithOne(y => y.Sender)
               .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public static void BuildNotification(this ModelBuilder builder)
        {
            builder.Entity<Notification>(notification =>
            {
                notification.HasOne(x => x.Sender)
                .WithMany(y => y.NotificationsSend)
                .OnDelete(DeleteBehavior.NoAction);

                notification.HasOne(x => x.Recipient)
               .WithMany(y => y.NotificationsRecived)
               .OnDelete(DeleteBehavior.NoAction);
            });
        }

        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Game>()
                .HasData(
                new Game { Id = Guid.NewGuid().ToString(), GameName = "lol", ImageUrl = "google.com" });

            var userId = Guid.NewGuid().ToString();
            User user = new User()
            {
                Id = userId,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin",
                NormalizedEmail = "ADMIN",
                LockoutEnabled = false,
            };
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, "admin123");
            
            builder.Entity<User>().HasData(user);

            var adminId = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole() { Id = adminId, Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" });

            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>() { RoleId = adminId, UserId = userId }
    );
        }
    }
}

