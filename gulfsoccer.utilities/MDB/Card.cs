using System;
using System.Collections.Generic;
using System.Text;

namespace gulfsoccer.utilities.MDB
{
    /// <summary>
    /// imgSrc: string, imgAlt: string, categoryLink: string, categoryTxt: string, title: string, 
    /// writerName: string, writerLink: string, lastUpdated: string, excerpt: string, 
    /// articleLink: string, readMoreText: string, dark:boolean
    /// </summary>
    public class Card
    {
        public string type { get; set; }
        public string imgSrc { get; set; }
        public string imgAlt { get; set; }
        public string categoryLink { get; set; }
        public string categoryTxt { get; set; }
        public string title { get; set; }
        public string writerName { get; set; }
        public string writerLink { get; set; }
        public string lastUpdated { get; set; }
        public string excerpt { get; set; }
        public string articleLink { get; set; }
        public string readMoreText { get; set; }
        public Boolean dark { get; set; }
    }
}
