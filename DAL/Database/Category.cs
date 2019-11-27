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
    }
}
