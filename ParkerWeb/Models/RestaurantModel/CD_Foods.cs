using ParkerWeb.Models.ResourcesModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace ParkerWeb.Models.RestaurantModel
{
    public class CD_Foods
    {
        public List<Foods> GetListFoods()
        {
            List<Foods> list = new List<Foods>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT f.foodID,f.foodName,f.foodDescription,");
                    sb.AppendLine("b.brandID, b.brandName,");
                    sb.AppendLine("c.categoryID,c.categoryName,");
                    sb.AppendLine("f.foodPrice,f.foodImageRoute,f.foodImageName,f.foodStatus");
                    sb.AppendLine("FROM Foods f");
                    sb.AppendLine("inner join FoodBrands b on b.brandID = f.brandID");
                    sb.AppendLine("inner join FoodCategories c on c.categoryID = f.categoryID");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), objConnection);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Foods()
                            {
                                foodID = Convert.ToInt32(dr["foodID"]),
                                foodName = dr["foodName"].ToString(),
                                foodDescription = dr["foodDescription"].ToString(),
                                objFoodBrands = new FoodBrands() { brandID = Convert.ToInt32(dr["brandID"]), brandName = dr["brandName"].ToString() },
                                objFoodCategory = new FoodCategories() { categoryID = Convert.ToInt32(dr["categoryID"]), categoryName = dr["categoryName"].ToString() },
                                foodPrice = Convert.ToDecimal(dr["foodPrice"], new CultureInfo("es-CR")),
                                foodImageRoute = dr["foodImageRoute"].ToString(),
                                foodImageName = dr["foodImageName"].ToString(),
                                foodStatus = Convert.ToBoolean(dr["foodStatus"])
                            });
                        }
                    }
                }
            }
            catch
            {
                list = new List<Foods>();
            }
            return list;
        }

        public int AddFood(Foods obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idFoodGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterFood", objConnection);
                    cmd.Parameters.AddWithValue("foodName", obj.foodName);
                    cmd.Parameters.AddWithValue("foodDescription", obj.foodDescription);
                    cmd.Parameters.AddWithValue("foodPrice", obj.foodPrice);
                    cmd.Parameters.AddWithValue("foodStatus", obj.foodStatus);
                    cmd.Parameters.AddWithValue("categoryID", obj.objFoodCategory.categoryID);
                    cmd.Parameters.AddWithValue("brandID", obj.objFoodBrands.brandID);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idFoodGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idFoodGenerate = 0;
                message = e.Message;
            }
            return idFoodGenerate;
        }

        public bool UpdateFood(Foods obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditFood", objConnection);
                    cmd.Parameters.AddWithValue("foodID", obj.foodID);
                    cmd.Parameters.AddWithValue("foodName", obj.foodName);
                    cmd.Parameters.AddWithValue("foodDescription", obj.foodDescription);
                    cmd.Parameters.AddWithValue("foodPrice", obj.foodPrice);
                    cmd.Parameters.AddWithValue("foodStatus", obj.foodStatus);
                    cmd.Parameters.AddWithValue("categoryID", obj.objFoodCategory.categoryID);
                    cmd.Parameters.AddWithValue("brandID", obj.objFoodBrands.brandID);
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


        public bool SaveDataImg(Foods obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "UPDATE Foods SET foodImageRoute = @foodImageRoute, foodImageName = @foodImageName WHERE foodID = @foodID";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.Parameters.AddWithValue("@foodImageRoute", obj.foodImageRoute);
                    cmd.Parameters.AddWithValue("@foodImageName", obj.foodImageName);
                    cmd.Parameters.AddWithValue("@foodID", obj.foodID);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        response = true;
                    }
                    else
                    {
                        message = "No se pudo guardar la imagen";
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

        public bool DeleteFood(int id, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteFood", objConnection);
                    cmd.Parameters.AddWithValue("foodID", id);
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