using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.LocationModel
{
    public class Cantons
    {
        public int cantonID { get; set; }
        public string cantonName { get; set; }
        public Provinces oProvincia { get; set; }
    }
}