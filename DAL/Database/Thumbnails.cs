using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Database
{
    public class Thumbnails
    {
        public int Id { get; set; }
        public int MediaId { get; set; }
        public virtual Media Media { get; set; }
        public int ThumbSizeId { get; set; }
        public virtual ThumbSize ThumbSize { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int rotate { get; set; }
        public int scaleX { get; set; }
        public int scaleY { get; set; }
        public int boxWidth { get; set; }
        public int boxHeight { get; set; }
    }
}
