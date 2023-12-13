using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.SalesModel
{
    public class Sales
    {
        public int saleID { get; set; }
        public int userID { get; set; }
        public int totalProducts { get; set; }
        public decimal totalPrice { get; set; }
        public string contact { get; set; }
        public int districtID { get; set; }
        public string saleCustomerNumber { get; set; }
        public string saleCustomerAddress { get; set; }
        public string saleTransactionID { get; set; }
        public string registrationDate { get; set; }
        public List<SalesDetail> objSalesDetail { get; set; }
    }
}