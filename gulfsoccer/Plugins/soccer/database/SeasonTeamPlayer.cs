using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class SeasonTeamPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SeasonTeamId { get; set; }
        public virtual SeasonTeam SeasonTeam { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public int PlayerPositionId { get; set; }
        public virtual PlayerPosition PlayerPosition { get; set; }

    }
}