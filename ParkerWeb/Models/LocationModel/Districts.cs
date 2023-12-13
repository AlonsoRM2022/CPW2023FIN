using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.LocationModel
{
    public class Districts
    {
        public int districtID { get; set; }
        public string districtZipCode { get; set; }
        public string districtName { get; set; }
        public Cantons oCanton { get; set; }
    }
}