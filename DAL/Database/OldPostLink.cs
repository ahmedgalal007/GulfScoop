using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Database
{
    public class OldPostLink
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PostId { get; set; }
        [Column(TypeName ="nvarchar")]
        public string Link { get; set; }
    }
}
