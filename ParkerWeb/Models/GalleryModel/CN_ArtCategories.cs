using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.GalleryModel
{
    public class CN_ArtCategories
    {
        private CD_ArtCategories objCapaDato = new CD_ArtCategories();    //  INSTANCIA DEL OBJETO
        public List<ArtCategories> CN_GetListArtCategories()
        {
            return objCapaDato.GetListArtCategories();
        }

        public int CN_AddCategory(ArtCategories obj, out string message)    //    REGISTRAR USUARIO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.categoryDescription) || string.IsNullOrWhiteSpace(obj.categoryDescription))
            {
                message = "El nombre de la categoria no puede estar vacio";
            }
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.AddArtCategories(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return 0;
            }
        }

        public bool CN_UpdateCategory(ArtCategories obj, out string message)
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.categoryDescription) || string.IsNullOrWhiteSpace(obj.categoryDescription))
            {
                message = "El nombre de la categoria no puede estar vacio";
            }
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.UpdateArtCategories(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return false;
            }
        }
    }
}