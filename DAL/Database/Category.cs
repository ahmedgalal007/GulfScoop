using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Database
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        // Foreign key relationship to parent category    
        public int? ParentId { get; set; }

        // Navigation property to parent category    
        public virtual Category Parent { get; set; }

        // Navigation property to child categories    
        public virtual ICollection<Category> Children { get; set; }

        public bool InMenu { get; set; }
        // #ff4444,#CC0000 - #ffbb33,#FF8800 - #00C851,#007E33 - #33b5e5,#0099CC - #2BBBAD,#00695c - #4285F4,#0d47a1 - #aa66cc,#9933CC
        // #2E2E2E,#212121 - #4B515D,#3E4551
        public string Color { get; set; }
    }
}
