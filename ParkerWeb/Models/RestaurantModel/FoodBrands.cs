using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.RestaurantModel
{
    public class FoodBrands
    {
        public int brandID { get; set; }
        public string brandName { get; set; }
        public bool brandStatus { get; set; }
        public string brandImageRoute { get; set; }
        public string brandImageName { get; set; }
        public string base64 { get; set; }
        public string extension { get; set; }
    }
}