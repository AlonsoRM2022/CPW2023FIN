using ParkerWeb.Models.ResourcesModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace ParkerWeb.Models.RestaurantModel
{
    public class CD_FoodCategories
    {
        public List<FoodCategories> GetListFoodCategories()
        {
            List<FoodCategories> list = new List<FoodCategories>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "SELECT categoryID,categoryName, categoryStatus FROM FoodCategories";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(
                                new FoodCategories()
                                {
                                    categoryID = Convert.ToInt32(dr["categoryID"]),
                                    categoryName = dr["categoryName"].ToString(),
                                    categoryStatus = Convert.ToBoolean(dr["categoryStatus"])
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                list = new List<FoodCategories>();
            }
            return list;
        }
        /*    METODO REGISTRAR      */
        public int AddFoodCategories(FoodCategories obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idFoodCategorieGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterFoodCategories", objConnection);  /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("categoryName", obj.categoryName);
                    cmd.Parameters.AddWithValue("categoryStatus", obj.categoryStatus);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idFoodCategorieGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idFoodCategorieGenerate = 0;
                message = e.Message;
            }
            return idFoodCategorieGenerate;
        }
        /*    METODO EDITAR      */
        public bool UpdateFoodCategories(FoodCategories obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditFoodCategories", objConnection);     /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("categoryID", obj.categoryID);
                    cmd.Parameters.AddWithValue("categoryName", obj.categoryName);
                    cmd.Parameters.AddWithValue("categoryStatus", obj.categoryStatus);
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
        /*  METODO ELIMINAR */
        public bool DeleteFoodCategories(int id, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteFoodCategories", objConnection);     /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("categoryID", id);
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

        public List<FoodCategories> GetListCategoryXBrand(int idbrand)
        {
            List<FoodCategories> list = new List<FoodCategories>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT distinct b.categoryID, b.categoryName from Foods p");
                    sb.AppendLine("inner join FoodBrands c on c.brandID = p.brandID");
                    sb.AppendLine("inner join FoodCategories b on b.categoryID = p.categoryID and b.categoryStatus = 1");
                    sb.AppendLine("where c.brandID = iif(@brand = 0, c.brandID, @brand);");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), objConnection);
                    cmd.Parameters.AddWithValue("@brand", idbrand);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(
                                new FoodCategories()
                                {
                                    categoryID = Convert.ToInt32(dr["categoryID"]),
                                    categoryName = dr["categoryName"].ToString()
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                list = new List<FoodCategories>();
            }
            return list;
        }
    }
}