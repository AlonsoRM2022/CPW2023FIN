using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.SalesModel
{
    public class CN_Reports
    {
        private CD_Reports objCapaDato = new CD_Reports();    //  INSTANCIA DEL OBJETO

        public List<Reports> Sales(string fechainicio, string fechafin, string idtransaccion)
        {
            return objCapaDato.Sales(fechainicio,fechafin,idtransaccion);
            //return objCapaDato.GetListReportSales(startDate, endDate, transactionID);
        }

        public Dashboard CN_GetListDashboard()   //  LISTAR 
        {
            return objCapaDato.GetListDashboard();
        }
    }
}