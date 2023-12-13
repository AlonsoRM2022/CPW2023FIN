using ParkerWeb.Models.ResourcesModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.LocationModel
{
    public class CD_Location
    {
        /*
         * --------------------------------------------------------------------------------
         * El siguiente metodo creará un listado de las provincias en el sistema
         * --------------------------------------------------------------------------------
         */
        public List<Provinces> GetListProvinces()
        {
            List<Provinces> list = new List<Provinces>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "select * from Provinces";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.CommandType = CommandType.Text;
                    // Cambiar por procedimiento almacenado ****
                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(
                                new Provinces()
                                {
                                    provinceID = Convert.ToInt32(dr["provinceID"]),
                                    provinceName = dr["provinceName"].ToString()
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                list = new List<Provinces>();
            }
            return list;
        }
        /*
         * --------------------------------------------------------------------------------
         * El siguiente metodo creará un listado de los cantones de provincia en el sistema
         * --------------------------------------------------------------------------------
         */
        public List<Cantons> GetListCantons(int provinceID)
        {
            List<Cantons> list = new List<Cantons>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "select * from Cantons where provinceID = @provinceID";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.Parameters.AddWithValue("@provinceID", provinceID);
                    cmd.CommandType = CommandType.Text;

                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(
                                new Cantons()
                                {
                                    cantonID = Convert.ToInt32(dr["cantonID"]),
                                    cantonName = dr["cantonName"].ToString()
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                list = new List<Cantons>();
            }
            return list;
        }
        /*
         * --------------------------------------------------------------------------------
         * El siguiente metodo creará un listado de los distritos de cantones en el sistema
         * --------------------------------------------------------------------------------
         */
        public List<Districts> GetListDistricts(int cantonID)
        {
            List<Districts> list = new List<Districts>();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(CD_Connection.db_Connection))
                {
                    string query = "select * from Districts where cantonID = @cantonID";
                    SqlCommand cmd = new SqlCommand(query, objConnection);
                    cmd.Parameters.AddWithValue("@cantonID", cantonID);
                    cmd.CommandType = CommandType.Text;

                    objConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(
                                new Districts()
                                {
                                    districtID = Convert.ToInt32(dr["districtID"]),
                                    districtZipCode = dr["districtZipCode"].ToString(),
                                    districtName = dr["districtName"].ToString()
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                list = new List<Districts>();
            }
            return list;
        }
    }
}