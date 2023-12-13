using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.GalleryModel
{
    public class CN_Art
    {
        private CD_Art objCapaDato = new CD_Art();    //  INSTANCIA DEL OBJETO
        public List<Art> CN_GetListArt()
        {
            return objCapaDato.GetListArt();
        }

        public List<Art> GetArtDetail(int id)
        {
            return objCapaDato.GetArtDetail(id);
        }

        public int CN_AddArt(Art obj, out string message)    //    REGISTRAR art
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.artist) || string.IsNullOrWhiteSpace(obj.artist))
            {
                message = "El nombre del artista no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.title) || string.IsNullOrWhiteSpace(obj.title))
            {
                message = "El titulo no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.artDescription) || string.IsNullOrWhiteSpace(obj.artDescription))
            {
                message = "La descripcion no puede ser vacio";
            }
            else if (obj.objArtCategory.categoryID == 0)
            {
                message = "Debe seleccionar una categoria";
            }
            //
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.AddArt(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return 0;
            }
        }

        public bool CN_UpdateArt(Art obj, out string message)     //  EDITAR FoodO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.artist) || string.IsNullOrWhiteSpace(obj.artist))
            {
                message = "El nombre del artista no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.title) || string.IsNullOrWhiteSpace(obj.title))
            {
                message = "El titulo no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.artDescription) || string.IsNullOrWhiteSpace(obj.artDescription))
            {
                message = "La descripcion no puede ser vacio";
            }
            else if (obj.objArtCategory.categoryID == 0)
            {
                message = "Debe seleccionar una categoria";
            }
            //
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.UpdateArt(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return false;
            }
        }

        public bool CN_SaveDataImg(Art obj, out string message)
        {
            return objCapaDato.SaveDataImg(obj, out message);
        }
    }
}