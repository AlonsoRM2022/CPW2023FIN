using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.UserModel
{
    public class Users
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string userLastName { get; set; }
        public string userMail { get; set; }
        public string userPassword { get; set; }
        public bool userRestore { get; set; }
        public bool userStatus { get; set; }
        public Roles objRol { get; set; }
    }
}