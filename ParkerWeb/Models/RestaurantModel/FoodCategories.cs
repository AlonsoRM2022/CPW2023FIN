using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.RestaurantModel
{
    public class FoodCategories
    {
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public bool categoryStatus { get; set; }
    }
}