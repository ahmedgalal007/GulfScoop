using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class SeasonWeek
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SeasonId { get; set; }
        public virtual Season Season { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public virtual List<Match> Matches { get; set; }

    }
}