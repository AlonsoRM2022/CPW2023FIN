using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.UserModel
{
    public class UsersLog
    {
        public int logID { get; set; }
        public Users objUser { get; set; }
        public string logDescription { get; set; }
    }
}