using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Database
{
    public class Media
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public string Localpath { get; set; }
        public string Keywords { get; set; }
        public string Type { get; set; }
        public string Alt { get; set; }
        public string Description { get; set; }
        public virtual List<Thumbnails> Thumbnails { get; set; }
    }
}
