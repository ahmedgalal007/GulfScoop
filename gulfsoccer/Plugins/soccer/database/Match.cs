using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class Match
    {
        public int Id { get; set; }
        public int FirstMatchTeamStatesId { get; set; }
        public virtual MatchTeamState FirstMatchTeamState { get; set; }
        public int SecondMatchTeamStatesId { get; set; }
        public virtual MatchTeamState SecondMatchTeamState { get; set; }
        public int StadiumId { get; set; }
        public virtual Stadium Stadium { get; set; }
        public DateTime Date { get; set; }


    }
}