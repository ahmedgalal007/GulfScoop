using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Database
{
    public partial class HeadAttr
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
