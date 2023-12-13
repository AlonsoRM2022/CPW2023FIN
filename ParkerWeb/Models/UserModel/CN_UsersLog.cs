using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.UserModel
{
    public class CN_UsersLog
    {
        private CD_UsersLog objCapaDato = new CD_UsersLog();
        public List<UsersLog> GetListUsersLog()
        {
            return objCapaDato.GetListUsersLog();
        }

        public int AddUsersLog(UsersLog obj, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrEmpty(obj.logDescription) || string.IsNullOrWhiteSpace(obj.logDescription))
            {
                message = "log";
            }
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.AddUsersLog(obj, out message);
            }
            else
            {
                return 0;
            }
        }

    }
}