using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Database
{
    public partial class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlaceType { get; set; }
        public int Place { get; set; }
        public string Organizer { get; set; }
    }
}