using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Database
{
    public partial class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
