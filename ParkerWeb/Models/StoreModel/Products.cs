using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.StoreModel
{
    public class Products
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public decimal productPrice { get; set; }
        public string productPricTxt { get; set; } // Revisar
        public int productStock { get; set; }
        public string productImageRoute { get; set; }
        public string productImageName { get; set; } // Revisar y quitar
        public bool productStatus { get; set; }
        public Brands objBrand { get; set; }
        public Categories objCategory { get; set; }
        public string base64 { get; set; }
        public string extension { get; set; }
    }
}