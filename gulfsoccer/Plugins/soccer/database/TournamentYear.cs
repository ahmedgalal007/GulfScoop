using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class TournamentYear
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public virtual List<SeasonTeam> SeasonTeams { get; set; }

    }
}