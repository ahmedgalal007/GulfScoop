using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class MatchPlayerPosition
    {
        public int Id { get; set; }
        public int MatchFormationId { get; set; }
        public virtual MatchFormation MatchFormation { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int PlayerPositionId { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
    }
}