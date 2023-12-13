using ParkerWeb.Models.ResourcesModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace ParkerWeb.Models.GalleryModel
{
    public class CD_Art
    {
        public List<Art> GetListArt()
        {
            List<Art> list = new List<Art>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT a.artID, a.artist, a.artDescription, a.title, ");
                    sb.AppendLine("ac.categoryID, ac.categoryDescription[Category],");
                    sb.AppendLine("a.ImageRoute, a.ImageName, a.artStatus");
                    sb.AppendLine("FROM Art a");
                    sb.AppendLine("INNER JOIN ArtCategories ac on ac.categoryID = a.categoryID");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), objConnection);
                    cmd.CommandType = CommandType.Text;

                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Art()
                            {
                                artID = Convert.ToInt32(dr["artID"]),
                                artist = dr["artist"].ToString(),
                                title = dr["title"].ToString(),
                                artDescription = dr["artDescription"].ToString(),
                                objArtCategory = new ArtCategories() { categoryID = Convert.ToInt32(dr["categoryID"]), categoryDescription = dr["Category"].ToString() },
                                ImageRoute = dr["ImageRoute"].ToString(),
                                ImageName = dr["ImageName"].ToString(),
                                artStatus = Convert.ToBoolean(dr["artStatus"]),
                            });
                        }
                    }
                }
            }
            catch
            {
                list = new List<Art>();
            }
            return list;
        }



        public List<Art> GetArtDetail(int id)
        {
            List<Art> list = new List<Art>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT artID, title, artDescription ");
                    sb.AppendLine("from Art");
                    sb.AppendLine("WHERE artID = @id");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), objConnection);
                    cmd.CommandType = CommandType.Text;

                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Art()
                            {
                                artID = Convert.ToInt32(dr["artID"]),
                                title = dr["title"].ToString(),
                                artDescription = dr["artDescription"].ToString(),
                            });
                        }
                    }
                }
            }
            catch
            {
                list = new List<Art>();
            }
            return list;
        }




        public int AddArt(Art obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idArtGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterArt", objConnection);
                    cmd.Parameters.AddWithValue("artist", obj.artist);
                    cmd.Parameters.AddWithValue("title", obj.title);
                    cmd.Parameters.AddWithValue("artDescription", obj.artDescription);
                    cmd.Parameters.AddWithValue("artStatus", obj.artStatus);
                    cmd.Parameters.AddWithValue("categoryID", obj.objArtCategory.categoryID);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idArtGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idArtGenerate = 0;
                message = e.Message;
            }
            return idArtGenerate;
        }

        public bool UpdateArt(Art obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditArt", objConnection);
                    cmd.Parameters.AddWithValue("artID", obj.artID);
                    cmd.Parameters.AddWithValue("artist", obj.artist);
                    cmd.Parameters.AddWithValue("title", obj.title);
                    cmd.Parameters.AddWithValue("artDescription", obj.artDescription);
                    cmd.Parameters.AddWithValue("artStatus", obj.artStatus);
                    cmd.Parameters.AddWithValue("categoryID", obj.objArtCategory.categoryID);
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

        public bool SaveDataImg(Art obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "UPDATE Art SET imageRoute = @imageRoute, ImageName = @ImageName WHERE artID = @artID";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.Parameters.AddWithValue("@imageRoute", obj.ImageRoute);
                    cmd.Parameters.AddWithValue("@ImageName", obj.ImageName);
                    cmd.Parameters.AddWithValue("@artID", obj.artID);
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