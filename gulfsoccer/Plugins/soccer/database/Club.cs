using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClubType { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public int CityId { get; set; }
        //public virtual City City { get; set; }
    }
}

public enum ClubType
{
    NationalTeam = 1,
    SportsClub = 2
}