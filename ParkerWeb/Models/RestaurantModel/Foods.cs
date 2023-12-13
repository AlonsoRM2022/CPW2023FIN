using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.RestaurantModel
{
    public class Foods
    {
        public int foodID { get; set; }
        public string foodName { get; set; }
        public string foodDescription { get; set; }
        public decimal foodPrice { get; set; }
        public string foodImageRoute { get; set; }
        public string foodImageName { get; set; }
        public bool foodStatus { get; set; }
        public FoodCategories objFoodCategory { get; set; }
        public FoodBrands objFoodBrands { get; set; }
        public string base64 { get; set; }
        public string extension { get; set; }
    }
}