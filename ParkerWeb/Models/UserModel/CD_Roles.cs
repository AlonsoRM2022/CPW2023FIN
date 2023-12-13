using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ParkerWeb.Models.ResourcesModel;

namespace ParkerWeb.Models.UserModel
{
    public class CD_Roles
    {
        /* 
         * La siguiente funcion contiene un metodo para listar los roles de usuarios
         */
        public List<Roles> GetListRoles()
        {
            List<Roles> list = new List<Roles>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "select * from Roles";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.CommandType = CommandType.Text;// Cambiar por procedimiento almacenado ****
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(
                                new Roles()
                                {
                                    rolID = Convert.ToInt32(dr["rolID"]),
                                    rolName = dr["rolName"].ToString()
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                list = new List<Roles>();
            }
            return list;
        }
    }
}