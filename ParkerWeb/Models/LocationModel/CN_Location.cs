using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.LocationModel
{
    public class CN_Location
    {
        private CD_Location objCapaDato = new CD_Location();

        public List<Provinces> GetProvinces()
        {
            return objCapaDato.GetListProvinces();
        }
        public List<Cantons> GetCantons(int provinceID)
        {
            return objCapaDato.GetListCantons(provinceID);
        }
        public List<Districts> GetDistricts(int cantonID)
        {
            return objCapaDato.GetListDistricts(cantonID);
        }
    }
}