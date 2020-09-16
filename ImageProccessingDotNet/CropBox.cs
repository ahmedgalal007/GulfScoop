using System;
using System.Collections.Generic;
using System.Text;

namespace ImageProccessingDotNet
{
    public class CropBox
    {
        public int ThumbSizeId { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int rotate { get; set; }
        public int scaleX { get; set; }
        public int scaleY { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int boxWidth { get; set; }
        public int boxHeight { get; set; }

        //public CropBox(int x, int y, int width, int height, int rotate, int scaleX, int scaleY, int left, int top, int boxWidth, int boxHeight)
        //{
        //    this.x = x;
        //    this.y = y;
        //    this.width = width;
        //    this.height = height;
        //    this.rotate = rotate;
        //    this.scaleX = scaleX;
        //    this.scaleY = scaleY;
        //    this.left = left;
        //    this.top = top;
        //    this.boxWidth = boxWidth;
        //    this.boxHeight = boxHeight;
        //}
        
    }
}
