using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class SeasonTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SeasonId { get; set; }
        public virtual Season Season { get; set; }
        public int ClubId { get; set; }
        public virtual Club Club { get; set; }


    }
}