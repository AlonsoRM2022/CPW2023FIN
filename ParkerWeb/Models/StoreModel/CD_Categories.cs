using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ParkerWeb.Models.ResourcesModel;
using System.Data.Common;


namespace ParkerWeb.Models.StoreModel
{
    public class CD_Categories
    {
        public List<Categories> GetListCategories()
        {
            List<Categories> list = new List<Categories>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "SELECT categoryID,categoryName, categoryStatus FROM Categories";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(
                                new Categories()
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
                list = new List<Categories>();
            }
            return list;
        }

        /*    METODO REGISTRAR      */
        public int AddCategorie(Categories obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idCategorieGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterCategories", objConnection);  /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("categoryName", obj.categoryName);
                    cmd.Parameters.AddWithValue("categoryStatus", obj.categoryStatus);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idCategorieGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idCategorieGenerate = 0;
                message = e.Message;
            }
            return idCategorieGenerate;
        }
        /*    METODO EDITAR      */
        public bool UpdateCategorie(Categories obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditCategories", objConnection);     /*     PROCEDIMIENTO ALMACENADO     */
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
        public bool DeleteCategorie(int id, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteCategories", objConnection);     /*     PROCEDIMIENTO ALMACENADO     */
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
    }
}