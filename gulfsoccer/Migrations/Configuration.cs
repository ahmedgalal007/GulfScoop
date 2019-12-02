namespace gulfsoccer.Migrations
{
    using DAL.Database;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<gulfsoccer.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(gulfsoccer.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Categories.AddOrUpdate(new Category { id = 1, name = "UnCategorized", ParentId = null, InMenu = false, Color = "#ff4444" });
            context.Categories.AddOrUpdate(new Category { id = 2, name = "Politics", ParentId = null, InMenu = false, Color = "#ffbb33" });
            context.Categories.AddOrUpdate(new Category { id = 3, name = "Egyptology", ParentId = null, InMenu = false, Color = "#00C851" });
            context.Categories.AddOrUpdate(new Category { id = 4, name = "Technology", ParentId = null, InMenu = false, Color = "#33b5e5" });
            context.Categories.AddOrUpdate(new Category { id = 5, name = "Marketing", ParentId = null, InMenu = false, Color = "#2BBBAD" });
            context.Categories.AddOrUpdate(new Category { id = 6, name = "Art", ParentId = null, InMenu = false, Color = "#4285F4" });
            context.Categories.AddOrUpdate(new Category { id = 9, name = "Heritage", ParentId = 6, InMenu = false, Color = "#aa66cc" });
            context.Categories.AddOrUpdate(new Category { id = 7, name = "Sports", ParentId = null, InMenu = false, Color = "#2E2E2E" });
            context.Categories.AddOrUpdate(new Category { id = 8, name = "Travel", ParentId = null, InMenu = false, Color = "#4B515D" });

            context.Tournaments.AddOrUpdate(new Tournament { Id = 1, Name = "Egyptian-Leage" });
            context.Clubs.AddOrUpdate(new Club { Id = 1, Name = "Ahly" });
            context.Players.AddOrUpdate(new Player { Id = 11, Name = "Mohamed Salah" });
        }
    }
}
