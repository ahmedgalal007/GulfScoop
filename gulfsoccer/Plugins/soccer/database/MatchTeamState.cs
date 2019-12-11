using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class MatchTeamState
    {
        public int Id { get; set; }
        //public int SeasonTeamId { get; set; }
        //public virtual SeasonTeam SeasonTeam { get; set; }
        public int MatchFormationId { get; set; }
        public MatchFormation MatchFormation { get; set; }
        public bool IsHome { get; set; }
        public bool IsAway { get; set; }
        public bool winner { get; set; }
        public int Points { get; set; }
    }
}