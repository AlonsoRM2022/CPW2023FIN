using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.BlogModel
{
    public class CN_Posts
    {
        private CD_Posts objCapaDato = new CD_Posts();    //  INSTANCIA DEL OBJETO
        public List<Posts> CN_GetListPosts()
        {
            return objCapaDato.GetListPosts();
        }

        //-----------------------------------------------------------------------------------------------
        public int CN_AddPost(Posts obj, out string message)    //    REGISTRAR FoodO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.postTitle) || string.IsNullOrWhiteSpace(obj.postTitle))
            {
                message = "El Titulo de la publicación no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.postContent) || string.IsNullOrWhiteSpace(obj.postContent))
            {
                message = "El contenido de la publicacion no puede ser vacio";
            }
            else if (obj.objUser.userID == 0)
            {
                message = "Debe iniciar sesion para publicar";
            }
            //
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.AddPost(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return 0;
            }
        }
        //-----------------------------------------------------------------------------------------------
        public bool CN_UpdatePost(Posts obj, out string message)     //  EDITAR FoodO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.postTitle) || string.IsNullOrWhiteSpace(obj.postTitle))
            {
                message = "El Titulo de la publicación no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.postContent) || string.IsNullOrWhiteSpace(obj.postContent))
            {
                message = "El contenido de la publicacion no puede ser vacio";
            }
            else if (obj.objUser.userID == 0)
            {
                message = "Debe iniciar sesion para publicar";
            }
            //
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.UpdatePost(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return false;
            }
        }

        public bool CN_SaveDataImg(Posts obj, out string message)
        {
            return objCapaDato.SaveDataImg(obj, out message);
        }




        public bool Delete(int id, out string message) 
        {
            return objCapaDato.Delete(id, out message);
        }
        //-----------------------------------------------------------------------------------------------
        //public bool CN_DeleteFood(int id, out string message)    //     ELIMINAR FoodO
        //{
        //    return objCapaDato.DeleteFood(id, out message);
        //}
    }
}