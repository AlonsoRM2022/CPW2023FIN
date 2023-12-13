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
    public class CD_FoodBrands
    {
        public List<FoodBrands> GetListFoodBrands()
        {
            List<FoodBrands> list = new List<FoodBrands>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT brandID,brandName,brandStatus,");
                    sb.AppendLine("brandImageRoute,brandImageName");
                    sb.AppendLine("FROM FoodBrands");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), objConnection);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new FoodBrands()
                            {
                                brandID = Convert.ToInt32(dr["brandID"]),
                                brandName = dr["brandName"].ToString(),
                                brandImageRoute = dr["brandImageRoute"].ToString(),
                                brandImageName = dr["brandImageName"].ToString(),
                                brandStatus = Convert.ToBoolean(dr["brandStatus"])
                            });
                        }
                    }
                }
            }
            catch
            {
                list = new List<FoodBrands>();
            }
            return list;
        }

        public int AddFoodBrands(FoodBrands obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idFBGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterFoodBrand", objConnection);
                    cmd.Parameters.AddWithValue("brandName", obj.brandName);
                    cmd.Parameters.AddWithValue("brandStatus", obj.brandStatus);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idFBGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idFBGenerate = 0;
                message = e.Message;
            }
            return idFBGenerate;
        }

        public bool UpdateFoodBrands(FoodBrands obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditFoodBrand", objConnection);
                    cmd.Parameters.AddWithValue("brandID", obj.brandID);
                    cmd.Parameters.AddWithValue("brandName", obj.brandName);
                    cmd.Parameters.AddWithValue("brandStatus", obj.brandStatus);
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


        public bool SaveDataImg(FoodBrands obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "UPDATE FoodBrands SET brandImageRoute = @brandImageRoute, brandImageName = @brandImageName WHERE brandID = @brandID";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.Parameters.AddWithValue("@brandImageRoute", obj.brandImageRoute);
                    cmd.Parameters.AddWithValue("@brandImageName", obj.brandImageName);
                    cmd.Parameters.AddWithValue("@brandID", obj.brandID);
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
    }
}