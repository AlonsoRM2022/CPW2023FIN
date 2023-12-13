using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkerWeb.Models.StoreModel;
using ParkerWeb.Models.UserModel;

namespace ParkerWeb.Models.SalesModel
{
    public class ShoppingCart
    {
        public int scID { get; set; }
        public int scQuantity { get; set; }
        public Users objCustomer { get; set; } // Objeto del cliente 
        public Products objProduct { get; set; } // Objeto del producto
    }
}