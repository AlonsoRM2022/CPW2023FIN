using ParkerWeb.Models.GalleryModel;
using ParkerWeb.Models.ResourcesModel;
using ParkerWeb.Models.StoreModel;
using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace ParkerWeb.Models.UserModel
{
    public class CD_UsersLog
    {

        //public List<UsersLog> GetListUsersLog()
        //{
        //    List<UsersLog> list = new List<UsersLog>();
        //    try
        //    {
        //        using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
        //        {
        //            StringBuilder sb = new StringBuilder();
        //            sb.AppendLine("SELECT ul.logID, u.userName[User], ul.logDescription");
        //            sb.AppendLine("FROM UsersLog ul");
        //            sb.AppendLine("INNER JOIN Users u on u.userID = ul.userID");

        //            SqlCommand cmd = new SqlCommand(sb.ToString(), objConnection);
        //            cmd.CommandType = CommandType.Text;

        //            objConnection.Open();
        //            using (SqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    list.Add(new UsersLog()
        //                    {
        //                        logID = Convert.ToInt32(dr["logID"]),
        //                        objUser = new Users() { userID = Convert.ToInt32(dr["userID"]), userName = dr["User"].ToString() },
        //                        logDescription = dr["logDescription"].ToString(),
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        list = new List<UsersLog>();
        //    }
        //    return list;
        //}


        public List<UsersLog> GetListUsersLog()
        {
            List<UsersLog> list = new List<UsersLog>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT ul.logID, ul.logDescription ");
                    sb.AppendLine("FROM UsersLog ul");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), objConnection);
                    cmd.CommandType = CommandType.Text;

                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new UsersLog()
                            {
                                logID = Convert.ToInt32(dr["logID"]),
                                logDescription = dr["logDescription"].ToString(),
                            });
                        }
                    }
                }
            }
            catch
            {
                list = new List<UsersLog>();
            }
            return list;
        }



        public int AddUsersLog(UsersLog obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idUsersLogGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterUserLog", objConnection);
                    cmd.Parameters.AddWithValue("userID", obj.objUser.userID);
                    cmd.Parameters.AddWithValue("logDescription", obj.logDescription);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idUsersLogGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idUsersLogGenerate = 0;
                message = e.Message;
            }
            return idUsersLogGenerate;
        }


    }
}