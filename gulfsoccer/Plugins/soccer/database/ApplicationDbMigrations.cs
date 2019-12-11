using DAL.Database;
using gulfsoccer.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace gulfsoccer.Plugins.soccer.database
{
    public static class ApplicationDbMigrations
    {
        public static void Seed(ApplicationDbContext context)
        {

            context.Categories.AddOrUpdate(new Category { id = 1, name = "UnCategorized", ParentId = null, InMenu = false, Color = "#ff4444" });
            context.Categories.AddOrUpdate(new Category { id = 2, name = "Politics", ParentId = null, InMenu = false, Color = "#ffbb33" });
            context.Categories.AddOrUpdate(new Category { id = 3, name = "Egyptology", ParentId = null, InMenu = false, Color = "#00C851" });
            context.Categories.AddOrUpdate(new Category { id = 4, name = "Technology", ParentId = null, InMenu = false, Color = "#33b5e5" });
            context.Categories.AddOrUpdate(new Category { id = 5, name = "Marketing", ParentId = null, InMenu = false, Color = "#2BBBAD" });
            context.Categories.AddOrUpdate(new Category { id = 6, name = "Art", ParentId = null, InMenu = false, Color = "#4285F4" });
            context.Categories.AddOrUpdate(new Category { id = 9, name = "Heritage", ParentId = 6, InMenu = false, Color = "#aa66cc" });
            context.Categories.AddOrUpdate(new Category { id = 7, name = "Sports", ParentId = null, InMenu = false, Color = "#2E2E2E" });
            context.Categories.AddOrUpdate(new Category { id = 8, name = "Travel", ParentId = null, InMenu = false, Color = "#4B515D" });

            context.Tournaments.AddOrUpdate(new Tournament { Id = 1, Name = "Egyptian-Leage", PlaceType = (int)PlaceType.Country, Place=1, Organizer="Egyptian Football Union" });
            
            context.Organizations.AddOrUpdate(new Organization { Id=1, Name="CAF", longName= "Confederation of African Football" });
            context.Organizations.AddOrUpdate(new Organization { Id = 2, Name = "FIFA", longName="" });
            context.Organizations.AddOrUpdate(new Organization { Id = 3, Name = "UEFA", longName = "" });
            context.Organizations.AddOrUpdate(new Organization { Id = 4, Name = "EPL", longName = "English Premier League" });
            context.Organizations.AddOrUpdate(new Organization { Id = 5, Name = "EFL", longName = "English Football League" });
            context.Organizations.AddOrUpdate(new Organization { Id = 6, Name = "AFC", longName = "Asian Football Confederation" });
            context.Organizations.AddOrUpdate(new Organization { Id = 7, Name = "European Leagues", longName = "European Leagues" });
            context.Organizations.AddOrUpdate(new Organization { Id = 8, Name = "Liga BBVA MX", longName = "Mexican football league system" });

            context.Continents.AddOrUpdate(new Continent { Id = 1, Name = "Africa" });
            context.Continents.AddOrUpdate(new Continent { Id = 2, Name = "Aisia" });
            context.Continents.AddOrUpdate(new Continent { Id = 3, Name = "Europe" });
            context.Continents.AddOrUpdate(new Continent { Id = 4, Name = "North America" });
            context.Continents.AddOrUpdate(new Continent { Id = 5, Name = "South America" });
            context.Continents.AddOrUpdate(new Continent { Id = 6, Name = "Austrailia" });

            context.Countries.AddOrUpdate(new Country { Id = 1, Name = "Egypt", ContinentId = 1 });
            context.Countries.AddOrUpdate(new Country { Id = 2, Name = "United Kingdom", ContinentId = 3 }); 

            context.CountryStates.AddOrUpdate(new CountryState { Id = 1, Name = "Egypt", CountryId = 1,  });
            context.Cities.AddOrUpdate(new City { Id = 1, Name = "Cairo", CountryStateId = 1 });

            context.Clubs.AddOrUpdate(new Club { Id = 1, Name = "Ahly", ClubType = (int)ClubType.SportsClub, CityId = 1, CountryId = 1 });
            context.Clubs.AddOrUpdate(new Club { Id = 2, Name = "Zamalek", ClubType = (int)ClubType.SportsClub, CityId = 1, CountryId = 1 });
            context.Players.AddOrUpdate(new Player { Id = 11, Name = "Mohamed Salah" });

            context.Roles.AddOrUpdate(new IdentityRole { Id = "1", Name = "Admin" });
            context.Roles.AddOrUpdate(new IdentityRole { Id = "2", Name = "Moderator" });
            context.Roles.AddOrUpdate(new IdentityRole { Id = "3", Name = "Editor" });
            context.Roles.AddOrUpdate(new IdentityRole { Id = "4", Name = "Subscriber" });
        }
    }
}