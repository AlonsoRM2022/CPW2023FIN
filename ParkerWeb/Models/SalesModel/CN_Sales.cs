using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.SalesModel
{
    public class CN_Sales
    {
        private CD_Sales objCapaDato = new CD_Sales();

        public bool AddSale(Sales obj, DataTable saleDetail, out string message)
        {
            return objCapaDato.AddSale(obj, saleDetail, out message);
        }

        public List<SalesDetail> GetListSales(int userID)
        {
            return objCapaDato.GetListSales(userID);
        }
    }
}