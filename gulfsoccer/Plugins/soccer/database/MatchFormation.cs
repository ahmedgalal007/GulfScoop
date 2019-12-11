using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class MatchFormation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MatchId { get; set; }
        public virtual Match Match { get; set; }
        public int SeasonTeamId { get; set; }
        public virtual SeasonTeam SeasonTeam { get; set; }
        public virtual List<MatchPlayerPosition> MatchPlayerPositions { get; set; }
    }
}