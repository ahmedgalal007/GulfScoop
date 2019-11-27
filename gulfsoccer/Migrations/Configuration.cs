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
            context.Categories.AddOrUpdate(new Category { id = 1, name = "UnCategorized", ParentId = null, InMenu = false });
            context.Categories.AddOrUpdate(new Category { id = 2, name = "Politics", ParentId = null, InMenu = false });
            context.Categories.AddOrUpdate(new Category { id = 3, name = "Egyptology", ParentId = null, InMenu = false });
            context.Categories.AddOrUpdate(new Category { id = 4, name = "Technology", ParentId = null, InMenu = false });
            context.Categories.AddOrUpdate(new Category { id = 5, name = "Marketing", ParentId = null, InMenu = false });
            context.Categories.AddOrUpdate(new Category { id = 6, name = "Art", ParentId = null, InMenu = false });
            context.Categories.AddOrUpdate(new Category { id = 9, name = "Heritage", ParentId = 6, InMenu = false });
            context.Categories.AddOrUpdate(new Category { id = 7, name = "Sports", ParentId = null, InMenu = false });
            context.Categories.AddOrUpdate(new Category { id = 8, name = "Travel", ParentId = null, InMenu = false });
        }
    }
}
