namespace gulfscoop.com.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<gulfscoop.com.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(gulfscoop.com.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Categories.AddOrUpdate(new Category { id = 1, name = "UnCategorized", ParentId = null, InMenu = false });
            context.Categories.AddOrUpdate(new Category { id = 2, name = "Politics", ParentId = null, InMenu = false });
            context.Categories.AddOrUpdate(new Category { id = 3, name = "Egypt", ParentId = 2, InMenu = false });
        }
    }
}
