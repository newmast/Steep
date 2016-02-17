namespace Steep.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;
    using Steep.Common;
    using System;
    public sealed class Configuration : DbMigrationsConfiguration<SteepDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SteepDbContext context)
        {
            const string AdministratorUserName = "admin";
            const string AdministratorEmail = "admin@admin.com";
            const string AdministratorPassword = AdministratorUserName;

            if (!context.Roles.Any() || context.Users.Count() == 0)
            {
                const string UserName = "admin";
                const string RoleName = "Administrator";

                var userRole = new IdentityRole { Name = RoleName, Id = Guid.NewGuid().ToString() };
                context.Roles.Add(userRole);

                var hasher = new PasswordHasher();

                var user = new User
                {
                    UserName = UserName,
                    PasswordHash = hasher.HashPassword(UserName),
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });

                context.Users.Add(user);
                context.SaveChanges();
            }

            if (!context.Genres.Any())
            {
                var genreNames = new[] { "Comedy", "Time travel", "Western", "Tragedy", "Horror", "Fanfiction", "Fantasy", "Action", "Drama" };

                for (int i = 0; i < genreNames.Length; i++)
                {
                    var genre = new Genre
                    {
                        Name = genreNames[i]
                    };

                    context.Genres.Add(genre);
                }

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);

                for (int i = 0; i < 3; i++)
                {
                    var story = new Story
                    {
                        AuthorId = userManager.Users.FirstOrDefault().Id,
                        Name = "This is story #" + i
                    };

                    context.Stories.Add(story);
                }

                context.SaveChanges();
            }
        }
    }
}
