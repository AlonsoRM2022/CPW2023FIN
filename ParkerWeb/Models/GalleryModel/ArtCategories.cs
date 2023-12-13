using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.GalleryModel
{
    public class ArtCategories
    {
        public int categoryID { get; set; }
        public string categoryDescription { get; set; }
        public bool categoryStatus { get; set; }
    }
}