using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using ParkerWeb.Models.ResourcesModel;


namespace ParkerWeb.Models.StoreModel
{
    public class CD_Brands
    {
        public List<Brands> GetListBrands()
        {
            List<Brands> list = new List<Brands>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "SELECT brandID, brandName, brandStatus FROM Brands";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(
                                new Brands()
                                {
                                    brandID = Convert.ToInt32(dr["brandID"]),
                                    brandName = dr["brandName"].ToString(),
                                    brandStatus = Convert.ToBoolean(dr["brandStatus"])
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                list = new List<Brands>();
            }
            return list;
        }
        /*    METODO REGISTRAR      */
        public int AddBrand(Brands obj, out string message)
        {
            /*      LIMPIEZA DE VARIABLES    */
            int idBrandGenerate = 0;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterBrand", objConnection);  /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("brandName", obj.brandName);
                    cmd.Parameters.AddWithValue("brandStatus", obj.brandStatus);
                    cmd.Parameters.Add("result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    objConnection.Open();
                    cmd.ExecuteNonQuery();
                    idBrandGenerate = Convert.ToInt32(cmd.Parameters["result"].Value);
                    message = cmd.Parameters["message"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                idBrandGenerate = 0;
                message = e.Message;
            }
            return idBrandGenerate;
        }
        /*    METODO EDITAR      */
        public bool UpdateBrand(Brands obj, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditBrand", objConnection);     /*     PROCEDIMIENTO ALMACENADO     */
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
        /*    METODO ELIMINAR      */
        public bool DeleteBrand(int IDBrand, out string message)
        {
            bool response = false;
            message = string.Empty;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteBrand", objConnection);     /*     PROCEDIMIENTO ALMACENADO     */
                    cmd.Parameters.AddWithValue("brandID", IDBrand);
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
        public List<Brands> GetListBrandXCategorie(int idCategory)
        {
            List<Brands> list = new List<Brands>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT distinct b.brandID, b.brandName from Products p");
                    sb.AppendLine("inner join Categories c on c.categoryID = p.categoryID");
                    sb.AppendLine("inner join Brands b on b.brandID = p.brandID and b.brandStatus = 1");
                    sb.AppendLine("where c.categoryID = iif(@categorie = 0, c.categoryID, @categorie);");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), objConnection);
                    cmd.Parameters.AddWithValue("@categorie", idCategory);
                    cmd.CommandType = CommandType.Text;
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(
                                new Brands()
                                {
                                    brandID = Convert.ToInt32(dr["brandID"]),
                                    brandName = dr["brandName"].ToString()
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                list = new List<Brands>();
            }
            return list;
        }
    }
}