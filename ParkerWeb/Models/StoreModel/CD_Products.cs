using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using ParkerWeb.Models.ResourcesModel;
using System.Data.Common;
using System.Text;

namespace ParkerWeb.Models.StoreModel
{
    public class CD_Products
    {
        public List<Products> GetListProducts()
        {
            List<Products> list = new List<Products>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT p.productID,p.productName,p.productDescription,");
                    sb.AppendLine("b.brandID, b.brandName[brand],");
                    sb.AppendLine("c.categoryID,c.categoryName[categorie],");
                    sb.AppendLine("p.productPrice,p.productStock,p.productImageRoute,p.productImageName,p.productStatus");
                    sb.AppendLine("FROM Products p");
                    sb.AppendLine("inner join Brands b on b.brandID = p.brandID");
                    sb.AppendLine("inner join Categories c on c.categoryID = p.categoryID");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), objConnection);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Products()
                            {
                                productID = Convert.ToInt32(dr["productID"]),
                                productName = dr["productName"].ToString(),
                                productDescription = dr["productDescription"].ToString(),
                                objBrand = new Brands() { brandID = Convert.ToInt32(dr["brandID"]), brandName = dr["brand"].ToString() },
                                objCategory = new Categories() { categoryID = Convert.ToInt32(dr["categoryID"]), categoryName = dr["categorie"].ToString() },
                                productPrice = Convert.ToDecimal(dr["productPrice"], new CultureInfo("es-CR")),
                                productStock = Convert.ToInt32(dr["productStock"]),
                                productImageRoute = dr["productImageRoute"].ToString(),
                                productImageName = dr["productImageName"].ToString(),
                                productStatus = Convert.ToBoolean(dr["productStatus"])
                            });
                        }
                    }
                }
            }
            catch
            {
                list = new List<Products>();
            }
            return list;
        }

        public int AddProduct(Products obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idProductGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterProduct", objConnection);
                    cmd.Parameters.AddWithValue("productName", obj.productName);
                    cmd.Parameters.AddWithValue("productDescription", obj.productDescription);
                    cmd.Parameters.AddWithValue("brandID", obj.objBrand.brandID);
                    cmd.Parameters.AddWithValue("categoryID", obj.objCategory.categoryID);
                    cmd.Parameters.AddWithValue("productPrice", obj.productPrice);
                    cmd.Parameters.AddWithValue("productStock", obj.productStock);
                    cmd.Parameters.AddWithValue("productStatus", obj.productStatus);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idProductGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idProductGenerate = 0;
                message = e.Message;
            }
            return idProductGenerate;
        }

        public bool UpdateProduct(Products obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditProduct", objConnection);
                    cmd.Parameters.AddWithValue("productID", obj.productID);
                    cmd.Parameters.AddWithValue("productName", obj.productName);
                    cmd.Parameters.AddWithValue("productDescription", obj.productDescription);
                    cmd.Parameters.AddWithValue("brandID", obj.objBrand.brandID);
                    cmd.Parameters.AddWithValue("categoryID", obj.objCategory.categoryID);
                    cmd.Parameters.AddWithValue("productPrice", obj.productPrice);
                    cmd.Parameters.AddWithValue("productStock", obj.productStock);
                    cmd.Parameters.AddWithValue("productStatus", obj.productStatus);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    response = Convert.ToBoolean(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                response = false;
                message = e.Message;
            }
            return response;
        }


        public bool SaveDataImg(Products obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "UPDATE Products SET productImageRoute = @productImageRoute, productImageName = @productImageName WHERE productID = @productID";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.Parameters.AddWithValue("@productImageRoute", obj.productImageRoute);
                    cmd.Parameters.AddWithValue("@productImageName", obj.productImageName);
                    cmd.Parameters.AddWithValue("@productID", obj.productID);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        response = true;
                    }
                    else
                    {
                        message = "No se pudo actualizar";
                    }
                }
            }
            catch (Exception e)
            {
                response = false;
                message = e.Message;
            }
            return response;
        }


        public bool DeleteProduct(int id, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteProduct", objConnection);
                    cmd.Parameters.AddWithValue("productID", id);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    response = Convert.ToBoolean(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                response = false;
                message = e.Message;
            }
            return response;
        }
    }
}