using ParkerWeb.Models.ResourcesModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.GalleryModel
{
    public class CD_ArtCategories
    {
        public List<ArtCategories> GetListArtCategories()
        {
            List<ArtCategories> list = new List<ArtCategories>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "SELECT categoryID,categoryDescription, categoryStatus FROM ArtCategories";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(
                                new ArtCategories()
                                {
                                    categoryID = Convert.ToInt32(dr["categoryID"]),
                                    categoryDescription = dr["categoryDescription"].ToString(),
                                    categoryStatus = Convert.ToBoolean(dr["categoryStatus"])
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                list = new List<ArtCategories>();
            }
            return list;
        }



        /*    METODO REGISTRAR      */
        public int AddArtCategories(ArtCategories obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idArtCategoryGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterArtCategories", objConnection);  /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("categoryDescription", obj.categoryDescription);
                    cmd.Parameters.AddWithValue("categoryStatus", obj.categoryStatus);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idArtCategoryGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idArtCategoryGenerate = 0;
                message = e.Message;
            }
            return idArtCategoryGenerate;
        }





        /*    METODO EDITAR      */
        public bool UpdateArtCategories(ArtCategories obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditArtCategories", objConnection);     /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("categoryID", obj.categoryID);
                    cmd.Parameters.AddWithValue("categoryDescription", obj.categoryDescription);
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
    }
}