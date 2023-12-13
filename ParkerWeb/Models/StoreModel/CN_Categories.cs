using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.StoreModel
{
    public class CN_Categories
    {
        private CD_Categories objCapaDato = new CD_Categories();    //  INSTANCIA DEL OBJETO
        public List<Categories> CN_GetListCategories()
        {
            return objCapaDato.GetListCategories();
        }

        //
        public int CN_AddCategorie(Categories obj, out string message)    //    REGISTRAR USUARIO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.categoryName) || string.IsNullOrWhiteSpace(obj.categoryName))
            {
                message = "El nombre de la categoria no puede estar vacio";
            }
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.AddCategorie(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return 0;
            }
        }
        public bool CN_UpdateCategorie(Categories obj, out string message)     //  EDITAR USUARIO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.categoryName) || string.IsNullOrWhiteSpace(obj.categoryName))
            {
                message = "El nombre de la categoria no puede estar vacio";
            }
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.UpdateCategorie(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return false;
            }
        }
        //
        public bool CN_DeleteCategorie(int id, out string message)    //     ELIMINAR USUARIO
        {
            return objCapaDato.DeleteCategorie(id, out message);
        }
    }
}