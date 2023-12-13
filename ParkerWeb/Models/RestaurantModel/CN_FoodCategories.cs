using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.RestaurantModel
{
    public class CN_FoodCategories
    {
        private CD_FoodCategories objCapaDato = new CD_FoodCategories();    //  INSTANCIA DEL OBJETO
        public List<FoodCategories> CN_GetListFoodCategories()
        {
            return objCapaDato.GetListFoodCategories();
        }

        //
        public int CN_AddCategorie(FoodCategories obj, out string message)    //    REGISTRAR USUARIO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.categoryName) || string.IsNullOrWhiteSpace(obj.categoryName))
            {
                message = "El nombre de la categoria no puede estar vacio";
            }
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.AddFoodCategories(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return 0;
            }
        }
        public bool CN_UpdateCategorie(FoodCategories obj, out string message)     //  EDITAR USUARIO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.categoryName) || string.IsNullOrWhiteSpace(obj.categoryName))
            {
                message = "El nombre de la categoria no puede estar vacio";
            }
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.UpdateFoodCategories(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return false;
            }
        }
        //
        public bool CN_DeleteCategorie(int id, out string message)    //     ELIMINAR USUARIO
        {
            return objCapaDato.DeleteFoodCategories(id, out message);
        }
        //
        public List<FoodCategories> CN_GetListCategoryXBrand(int idbrand)
        {
            return objCapaDato.GetListCategoryXBrand(idbrand);
        }
    }
}