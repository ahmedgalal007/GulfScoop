using DAL.Database;
using gulfsoccer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gulfsoccer
{
    public class Settings: IDisposable
    {
        private static ApplicationDbContext _db = new ApplicationDbContext();
        private static Settings _settings;
        public static List<Tournament> Tournaments;
        public static List<Club> Clubs;
        public static List<Player> Players;

        public static Settings Create()
        {
            _settings = _settings == null ? new Settings(): _settings;
            // _db = _db == null ? new ApplicationDbContext(): _db;
            Tournaments = Tournaments == null ? _db.Tournaments.ToList(): Tournaments;
            Clubs = Clubs == null? _db.Clubs.ToList(): Clubs;
            Players = Players==null? _db.Players.ToList(): Players;

            return _settings;
        }

        public void Dispose()
        {
            // _db.Dispose();
            // Tournaments = null;
            // Clubs = null;
            // Players = null;
            // _settings = null;
        }
    }
}