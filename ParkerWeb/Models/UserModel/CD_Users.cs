using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using ParkerWeb.Models.ResourcesModel;

namespace ParkerWeb.Models.UserModel
{
    public class CD_Users
    {
        /* 
        * La siguiente funcion contiene un metodo para listar los usuarios
        */
        public List<Users> GetListUsers()
        {
            List<Users> list = new List<Users>(); //Instancia de la Lista
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection)) // Instancia de la Conexion a la DB
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT u.userID, u.userName, u.userLastName, u.userMail, u.userPassword, u.userRestore, u.userStatus,");
                    sb.AppendLine("r.rolID, r.rolName");
                    sb.AppendLine("FROM Users u");
                    sb.AppendLine("inner join Roles r on r.rolID = u.rolID");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), objConnection);
                    cmd.CommandType = CommandType.Text; // Tipo de Comando [ **CAMBIAR POR PROCEDIMIENTO ALMACENADO ]
                    objConnection.Open(); // abre la conexion                   
                    using (SqlDataReader dr = cmd.ExecuteReader()) // Lectura de los datos devueltos
                    {
                        while (dr.Read())
                        {
                            list.Add( // Agregar Usuarios a la Lista
                                new Users() //Objeto Usuario y sus atributos
                                {
                                    userID = Convert.ToInt32(dr["userID"]),
                                    userName = dr["userName"].ToString(),
                                    userLastName = dr["userLastName"].ToString(),
                                    userMail = dr["userMail"].ToString(),
                                    userPassword = dr["userPassword"].ToString(),
                                    userRestore = Convert.ToBoolean(dr["userRestore"]),
                                    userStatus = Convert.ToBoolean(dr["userStatus"]),
                                    objRol = new Roles() { rolID = Convert.ToInt32(dr["rolID"]), rolName = dr["rolName"].ToString() },
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                list = new List<Users>(); // En caso de error devuelve una lista vacia
            }
            return list;
        }
        /* 
         * La siguiente funcion contiene un metodo para listar los clientes
         */
        public List<Users> GetListCustomers()
        {
            List<Users> list = new List<Users>(); //Instancia de la Lista
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection)) // Instancia de la Conexion a la DB
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT u.userID, u.userName, u.userLastName, u.userMail, u.userPassword, u.userRestore, u.userStatus,");
                    sb.AppendLine("r.rolID, r.rolName");
                    sb.AppendLine("FROM Users u");
                    sb.AppendLine("inner join Roles r on r.rolID = u.rolID");
                    sb.AppendLine(" Where u.rolID = 2"); // **Probar este where
                    SqlCommand cmd = new SqlCommand(sb.ToString(), objConnection);
                    cmd.CommandType = CommandType.Text; // Tipo de Comando [ **CAMBIAR POR PROCEDIMIENTO ALMACENADO ]
                    objConnection.Open(); // abre la conexion
                    using (SqlDataReader dr = cmd.ExecuteReader()) // Lectura de los datos devueltos
                    {
                        while (dr.Read())
                        {
                            list.Add( // Agregar Usuarios a la Lista
                                new Users() //Objeto Usuario y sus atributos
                                {
                                    userID = Convert.ToInt32(dr["userID"]),
                                    userName = dr["userName"].ToString(),
                                    userLastName = dr["userLastName"].ToString(),
                                    userMail = dr["userMail"].ToString(),
                                    userPassword = dr["userPassword"].ToString(),
                                    userRestore = Convert.ToBoolean(dr["userRestore"]),
                                    userStatus = Convert.ToBoolean(dr["userStatus"]),
                                    objRol = new Roles() { rolID = Convert.ToInt32(dr["rolID"]), rolName = dr["rolName"].ToString() },
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                list = new List<Users>(); // En caso de error devuelve una lista vacia
            }
            return list;
        }
        /*    METODO REGISTRAR UN NUEVO USUARIO      */
        public int AddUser(Users obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idUserGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterUser", objConnection);  /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("userName", obj.userName);
                    cmd.Parameters.AddWithValue("userLastName", obj.userLastName);
                    cmd.Parameters.AddWithValue("userMail", obj.userMail);
                    cmd.Parameters.AddWithValue("userPassword", obj.userPassword);
                    cmd.Parameters.AddWithValue("userStatus", obj.userStatus);
                    cmd.Parameters.AddWithValue("rolID", obj.objRol.rolID);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idUserGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idUserGenerate = 0;
                message = e.Message;
            }
            return idUserGenerate;
        }
        public int AddCustomer(Users obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idUserGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterCustomer", objConnection);  /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("userName", obj.userName);
                    cmd.Parameters.AddWithValue("userLastName", obj.userLastName);
                    cmd.Parameters.AddWithValue("userMail", obj.userMail);
                    cmd.Parameters.AddWithValue("userPassword", obj.userPassword);
                    cmd.Parameters.AddWithValue("userStatus", obj.userStatus);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idUserGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idUserGenerate = 0;
                message = e.Message;
            }
            return idUserGenerate;
        }
        /*    METODO EDITAR UN USUARIO      */
        public bool UpdateUser(Users obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditUser", objConnection);     /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("userID", obj.userID);
                    cmd.Parameters.AddWithValue("userName", obj.userName);
                    cmd.Parameters.AddWithValue("userLastName", obj.userLastName);
                    cmd.Parameters.AddWithValue("userMail", obj.userMail);
                    cmd.Parameters.AddWithValue("userStatus", obj.userStatus);
                    cmd.Parameters.AddWithValue("rolID", obj.objRol.rolID);
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
        /*     METODO ELIMINAR USUARIO      */
        public bool DeleteUser(int userID, out string message)
        {
            bool result = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("DELETE TOP(1) FROM Users WHERE userID = @id", objConnection);
                    cmd.Parameters.AddWithValue("@id", userID);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    result = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception e)
            {
                result = false;
                message = e.Message;
            }
            return result;
        }
        /*  METODO  CAMBIAR CONTRASEÑA*/
        public bool ChangePassword(int userID, string password, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET userPassword = @newPassword, userRestore = 0 WHERE userID = @id", objConnection);
                    cmd.Parameters.AddWithValue("@id", userID);
                    cmd.Parameters.AddWithValue("@newPassword", password);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    response = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception e)
            {
                response = false;
                message = e.Message;
            }
            return response;
        }
        /*  METODO RESTABLECER CONTRASEÑA
         *  al ingresar por primera vez al sistema
         *  para cambiar la contraseña autogenerada
         */
        public bool ResetPassword(int userID, string password, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET userPassword = @newPassword, userRestore = 1 WHERE userID = @id", objConnection);
                    cmd.Parameters.AddWithValue("@id", userID);
                    cmd.Parameters.AddWithValue("@newPassword", password);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    response = cmd.ExecuteNonQuery() > 0 ? true : false;
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