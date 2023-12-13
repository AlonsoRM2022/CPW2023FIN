using ParkerWeb.Models.StoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.SalesModel
{
    public class SalesDetail
    {
        public int salesDetailID { get; set; }
        public int salesDetailQuantity { get; set; }
        public decimal total { get; set; } 
        public int saleID { get; set; }
        public Products objProduct { get; set; }
        public string saleTransactionID { get; set; }
    }
}