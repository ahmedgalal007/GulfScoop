using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Database
{
    public class ThumbSize
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AspectRatio { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
