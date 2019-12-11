using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class Continent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Country> Countries { get; set; }
    }

    public enum PlaceType
    {
        Continent = 1,
        Country = 2,
        State = 3,
        City = 4
    }
}