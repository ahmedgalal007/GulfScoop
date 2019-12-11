using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryStateId { get; set; }
        public virtual CountryState CountryState { get; set; }
        public virtual List<Club> Clubs { get; set; }
    }
}