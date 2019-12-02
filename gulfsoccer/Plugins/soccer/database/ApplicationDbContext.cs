using DAL.Database;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace gulfsoccer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchFormation> MatchFormations { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerPosition> PlayerPositions { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SeasonTeam> SeasonTeams { get; set; }
        public DbSet<SeasonWeek> SeasonWeeks { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Transition> Transitions { get; set; }
        public DbSet<Trophy> Trophies { get; set; }
        
    }
}