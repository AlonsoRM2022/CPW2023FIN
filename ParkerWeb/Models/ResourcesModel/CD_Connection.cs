using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.ResourcesModel
{
    public class CD_Connection
    {
        public static string db_Connection = ConfigurationManager.ConnectionStrings["db_connection"].ToString();
    }
}