using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.SalesModel
{
    public class Reports
    {
        //public string saleDate { get; set; }
        //public string customer { get; set; }
        //public string product { get; set; }
        //public decimal price { get; set; }
        //public int quantity { get; set; }
        //public decimal total { get; set; }
        //public string transactionID { get; set; }

        public string FechaVenta { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public decimal productPrice { get; set; }
        public int salesDetailQuantity { get; set; }
        public decimal total { get; set; }
        public string saleTransactionID { get; set; }

    }
}