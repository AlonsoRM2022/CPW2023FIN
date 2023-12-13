using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.UserModel
{
    public class CN_Roles
    {
        private CD_Roles objCapaDato = new CD_Roles();    //  INSTANCIA DEL OBJETO
        public List<Roles> CN_GetListRoles()   //  LISTAR USUARIOS
        {
            return objCapaDato.GetListRoles();
        }
    }
}