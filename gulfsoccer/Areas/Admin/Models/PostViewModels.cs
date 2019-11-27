using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gulfsoccer.Areas.Admin.Models.PostViewModels
{
    public class CreatePostViewModel
    {
        public int id { get; set; }
        public string title { get; set; }

        public string body { get; set; }

        public DateTime created { get; set; }
        public DateTime updated { get; set; }

        public string categories { get; set; }
        public string tags { get; set; }

        public string featuredImage { get; set; }

        public string owner { get; set; }
        public string permalink { get; set; }
    }
}