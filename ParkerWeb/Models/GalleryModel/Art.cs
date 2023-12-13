using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.GalleryModel
{
    public class Art
    {
        public int artID { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public string artDescription { get; set; }
        public string ImageRoute { get; set; }
        public string ImageName { get; set; }
        public bool artStatus { get; set; }
        public ArtCategories objArtCategory { get; set; }
        public string base64 { get; set; }
        public string extension { get; set; }
    }
}