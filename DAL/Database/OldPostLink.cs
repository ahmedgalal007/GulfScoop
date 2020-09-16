using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Database
{
    public class OldPostLink
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PostId { get; set; }
        public string Link { get; set; }
    }
}
