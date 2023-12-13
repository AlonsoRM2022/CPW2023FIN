using ParkerWeb.Models.ResourcesModel;
using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace ParkerWeb.Models.BlogModel
{
    public class CD_Posts
    {
        public List<Posts> GetListPosts()
        {
            List<Posts> list = new List<Posts>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT p.postID,p.postTitle,p.postContent,p.registrationDate,postStatus,");
                    sb.AppendLine("u.userID, u.userName");
                    sb.AppendLine("FROM Posts p");
                    sb.AppendLine("inner join Users u on u.userID = p.userID");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), objConnection);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Posts()
                            {
                                postID = Convert.ToInt32(dr["postID"]),
                                postTitle = dr["postTitle"].ToString(),
                                postContent = dr["postContent"].ToString(),
                                postStatus = Convert.ToBoolean(dr["postStatus"].ToString()),
                                postDate = Convert.ToDateTime(dr["registrationDate"]),
                                objUser = new Users() { userID = Convert.ToInt32(dr["userID"]), userName = dr["userName"].ToString() }
                            });
                        }
                    }
                }
            }
            catch
            {
                list = new List<Posts>();
            }
            return list;
        }

        public int AddPost(Posts obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idPostGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterPost", objConnection);
                    cmd.Parameters.AddWithValue("postTitle", obj.postTitle);
                    cmd.Parameters.AddWithValue("postContent", obj.postContent);
                    cmd.Parameters.AddWithValue("postStatus", obj.postStatus);
                    cmd.Parameters.AddWithValue("userID", obj.objUser.userID);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idPostGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idPostGenerate = 0;
                message = e.Message;
            }
            return idPostGenerate;
        }

        public bool UpdatePost(Posts obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditPost", objConnection);
                    cmd.Parameters.AddWithValue("postTitle", obj.postTitle);
                    cmd.Parameters.AddWithValue("postContent", obj.postContent);
                    cmd.Parameters.AddWithValue("postStatus", obj.postStatus);
                    cmd.Parameters.AddWithValue("userID", obj.objUser.userID);
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

        public bool SaveDataImg(Posts obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "UPDATE Posts SET postImageRoute = @postImageRoute, postImageName = @postImageName WHERE postID = @postID";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.Parameters.AddWithValue("@postImageRoute", obj.postImageRoute);
                    cmd.Parameters.AddWithValue("@postImageName", obj.postImageName);
                    cmd.Parameters.AddWithValue("@postID", obj.postID);
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