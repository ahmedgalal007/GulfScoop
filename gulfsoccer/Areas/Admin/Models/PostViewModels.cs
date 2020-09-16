using DAL.Database;
using System;
using System.Collections.Generic;
using Telerik.Windows.Documents.Fixed.Model.Editing.Lists;

namespace gulfsoccer.Areas.Admin.Models.PostViewModels
{
    public class CreatePostViewModel
    {
        public int id { get; set; }
        public string title { get; set; }

        public string body { get; set; }
        public string description { get; set; }

        public DateTime created { get; set; }
        public DateTime updated { get; set; }

        public string categories { get; set; }
        public string tags { get; set; }

        public string featuredImage { get; set; }
        public string featuredImagethumbs { get; set; }
        public string owner { get; set; }
        public string mainCategory { get; set; }
        public string permalink { get; set; }
    }
}