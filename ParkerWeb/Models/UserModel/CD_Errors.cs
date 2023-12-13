using ParkerWeb.Models.ResourcesModel;
using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.UserModel
{
    public class CD_Errors
    {

        public List<Errors> GetListErrors()
        {
            List<Errors> list = new List<Errors>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "SELECT errorID,errorDescription FROM Errors";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(
                                new Errors()
                                {
                                    errorID = Convert.ToInt32(dr["errorID"]),
                                    errorDescription = dr["errorDescription"].ToString()
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                list = new List<Errors>();
            }
            return list;
        }


        public int AddErrors(Errors obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idErrorsGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_ErrorRegister", objConnection);  /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("errorDescription", obj.errorDescription);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idErrorsGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idErrorsGenerate = 0;
                message = e.Message;
            }
            return idErrorsGenerate;
        }


    }
}