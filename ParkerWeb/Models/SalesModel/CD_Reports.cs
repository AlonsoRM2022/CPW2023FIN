using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using ParkerWeb.Models.ResourcesModel;

namespace ParkerWeb.Models.SalesModel
{
    public class CD_Reports
    {
        /*          */
        public List<Reports> Sales(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reports> list = new List<Reports>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReportSales", objConnection);
                    cmd.Parameters.AddWithValue("fechaInicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechaFin", fechafin);
                    cmd.Parameters.AddWithValue("idTransaccion", idtransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(
                                new Reports()
                                {
                                    FechaVenta = dr["FechaVenta"].ToString(),
                                    Cliente = dr["Cliente"].ToString(),
                                    Producto = dr["Producto"].ToString(),
                                    productPrice = Convert.ToDecimal(dr["productPrice"], new CultureInfo("es-PE")),
                                    salesDetailQuantity = Convert.ToInt32(dr["salesDetailQuantity"].ToString()),
                                    total = Convert.ToDecimal(dr["total"], new CultureInfo("es-PE")),
                                    saleTransactionID = dr["saleTransactionID"].ToString()
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                list = new List<Reports>();
            }
            return list;
        }
        /*          */
        public Dashboard GetListDashboard()
        {
            Dashboard objDashboard = new Dashboard();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", objConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objDashboard = new Dashboard()
                            {
                                totalCustomers = Convert.ToInt32(dr["totalCustomers"]),
                                totalSales = Convert.ToInt32(dr["totalSales"]),
                                totalProducts = Convert.ToInt32(dr["totalProducts"])
                            };
                        }
                    }
                }
            }
            catch
            {
                objDashboard = new Dashboard();
            }
            return objDashboard;
        }
    }
}