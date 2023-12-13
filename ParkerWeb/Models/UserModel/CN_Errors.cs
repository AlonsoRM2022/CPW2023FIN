using ParkerWeb.Models.StoreModel;
using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.UserModel
{
    public class CN_Errors
    {
        private CD_Errors objCapaDato = new CD_Errors();
        public List<Errors> GetListErrors()
        {
            return objCapaDato.GetListErrors();
        }

        public int AddErrors(Errors obj, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrEmpty(obj.errorDescription) || string.IsNullOrWhiteSpace(obj.errorDescription))
            {
                message = "Error";
            }
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.AddErrors(obj, out message);
            }
            else
            {
                return 0;
            }
        }

    }
}