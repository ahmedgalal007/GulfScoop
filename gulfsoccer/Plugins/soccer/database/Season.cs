using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class Season
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TournamentYearId { get; set; }
        public virtual TournamentYear TournamentYear { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}