using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using ParkerWeb.Models.StoreModel;
using ParkerWeb.Models.ResourcesModel;

namespace ParkerWeb.Models.SalesModel
{
    public class CD_ShoppingCart
    {
        public bool ExistCart(int userID, int productID)
        {
            bool result = true;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_ExistCart", objConnection);
                    cmd.Parameters.AddWithValue("userID", userID);
                    cmd.Parameters.AddWithValue("productID", productID);
                    cmd.Parameters.Add("result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(cmd.Parameters["result"].Value);
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }
        public bool CartOperation(int userID, int productID, bool add, out string message)
        {
            bool result = true;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_OperationCart", objConnection);
                    cmd.Parameters.AddWithValue("userID", userID);
                    cmd.Parameters.AddWithValue("productID", productID);
                    cmd.Parameters.AddWithValue("add", add);
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
        public int CartQuantity(int userID)
        {
            int result = 0;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("SELECT count(*) FROM ShoppingCart WHERE userID = @userID", objConnection);
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                result = 0;
            }
            return result;
        }
        public List<ShoppingCart> GetProductList(int userID)
        {
            List<ShoppingCart> list = new List<ShoppingCart>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_AskCustomerCart", objConnection);
                    cmd.Parameters.AddWithValue("userID", userID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new ShoppingCart()
                            {
                                objProduct = new Products()
                                {
                                    productID = Convert.ToInt32(dr["productID"]),
                                    productName = dr["productName"].ToString(),
                                    objBrand = new Brands() { brandName = dr["DesMarca"].ToString() },
                                    //objCategory = new Categories() { categoryID = Convert.ToInt32(dr["categorieID"]), categoryName = dr["categorie"].ToString() },
                                    productPrice = Convert.ToDecimal(dr["productPrice"], new CultureInfo("es-CR")),
                                    productImageRoute = dr["productImageRoute"].ToString(),
                                    productImageName = dr["productImageName"].ToString()
                                },
                                scQuantity = Convert.ToInt32(dr["scQuantity"])
                            });
                        }
                    }
                }
            }
            catch
            {
                list = new List<ShoppingCart>();
            }
            return list;
        }
        public bool DeleteCart(int userID, int productID)
        {
            bool result = true;

            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteCart", objConnection);
                    cmd.Parameters.AddWithValue("userID", userID);
                    cmd.Parameters.AddWithValue("productID", productID);
                    cmd.Parameters.Add("result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(cmd.Parameters["result"].Value);
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }
    }
}