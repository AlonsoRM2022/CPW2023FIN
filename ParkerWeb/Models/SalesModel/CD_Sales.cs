using ParkerWeb.Models.ResourcesModel;
using ParkerWeb.Models.StoreModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.SalesModel
{
    public class CD_Sales
    {
        public bool AddSale(Sales obj, DataTable saleDetail, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("usp_RegisterSale", objConnection);     /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("userID", obj.userID);
                    cmd.Parameters.AddWithValue("totalProducts", obj.totalProducts);
                    cmd.Parameters.AddWithValue("totalPrice", obj.totalPrice);
                    cmd.Parameters.AddWithValue("districtID", obj.districtID);
                    cmd.Parameters.AddWithValue("contact", obj.contact);
                    cmd.Parameters.AddWithValue("saleCustomerNumber", obj.saleCustomerNumber);
                    cmd.Parameters.AddWithValue("saleCustomerAddress", obj.saleCustomerAddress);
                    cmd.Parameters.AddWithValue("saleTransactionID", obj.saleTransactionID);
                    cmd.Parameters.AddWithValue("SalesDetail", saleDetail);
                    cmd.Parameters.Add("result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                result = false;
                message = e.Message;
            }
            return result;
        }
        public List<SalesDetail> GetListSales(int userID)
        {
            List<SalesDetail> list = new List<SalesDetail>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "SELECT * FROM fn_ListBuy(@userID)";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new SalesDetail()
                            {
                                objProduct = new Products()
                                {
                                    productName = dr["productName"].ToString(),
                                    productPrice = Convert.ToDecimal(dr["productPrice"], new CultureInfo("es-CR")),
                                    productImageRoute = dr["productImageRoute"].ToString(),
                                    productImageName = dr["productImageName"].ToString()
                                },
                                salesDetailQuantity = Convert.ToInt32(dr["salesDetailQuantity"]),
                                total = Convert.ToDecimal(dr["total"], new CultureInfo("es-CR")), //*** Revisar
                                saleTransactionID = dr["saleTransactionID"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                list = new List<SalesDetail>();
            }
            return list;
        }
    }
}