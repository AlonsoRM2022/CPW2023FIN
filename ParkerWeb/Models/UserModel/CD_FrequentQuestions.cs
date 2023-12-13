using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ParkerWeb.Models.ResourcesModel;

namespace ParkerWeb.Models.UserModel
{
    public class CD_FrequentQuestions
    {
            public List<FrequentQuestions> GetListFrequentQuestions()
            {
                List<FrequentQuestions> list = new List<FrequentQuestions>();
                try
                {
                    using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                    {
                        string query = "SELECT questionID,questionTitle,questionBody, questionStatus FROM FrequentQuestions";
                        SqlCommand cmd = new SqlCommand(query, objConnection);
                        cmd.CommandType = CommandType.Text;
                        objConnection.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(
                                    new FrequentQuestions()
                                    {
                                        questionID = Convert.ToInt32(dr["questionID"]),
                                        questionTitle = dr["questionTitle"].ToString(),
                                        questionBody = dr["questionBody"].ToString(),
                                        questionStatus = Convert.ToBoolean(dr["questionStatus"])
                                    }
                                );
                            }
                        }
                    }
                }
                catch
                {
                    list = new List<FrequentQuestions>();
                }
                return list;
            }



            /*    METODO REGISTRAR      */
            public int AddFrequentQuestions(FrequentQuestions obj, out string message)
            {
                /*      LIMPIEZA DE VARIABLES    */
                int idFrequentQuestionGenerate = 0;
                message = string.Empty;
                try
                {
                    using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                    {
                        SqlCommand cmd = new SqlCommand("sp_RegisterFrequentQuestions", objConnection);  /*     PROCEDIMIENTO ALMACENADO     */
                        cmd.Parameters.AddWithValue("questionTitle", obj.questionTitle);
                        cmd.Parameters.AddWithValue("questionBody", obj.questionBody);
                        cmd.Parameters.AddWithValue("questionStatus", obj.questionStatus);
                        cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;
                        objConnection.Open();
                        cmd.ExecuteNonQuery();
                        idFrequentQuestionGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                        message = cmd.Parameters["message"].Value.ToString();
                    }
                }
                catch (Exception e)
                {
                    idFrequentQuestionGenerate = 0;
                    message = e.Message;
                }
                return idFrequentQuestionGenerate;
            }





            /*    METODO EDITAR      */
            public bool UpdateFrequentQuestions(FrequentQuestions obj, out string message)
            {
                bool response = false;
                message = string.Empty;
                try
                {
                    using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                    {
                        SqlCommand cmd = new SqlCommand("sp_EditFrequentQuestions", objConnection);     /*     PROCEDIMIENTO ALMACENADO     */
                        cmd.Parameters.AddWithValue("questionID", obj.questionID);
                        cmd.Parameters.AddWithValue("questionTitle", obj.questionTitle);
                        cmd.Parameters.AddWithValue("questionBody", obj.questionBody);
                        cmd.Parameters.AddWithValue("questionStatus", obj.questionStatus);
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