using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.RestaurantModel
{
    public class CN_FoodBrands
    {
        private CD_FoodBrands objCapaDato = new CD_FoodBrands();    //  INSTANCIA DEL OBJETO
        public List<FoodBrands> CN_GetListFoodBrands()
        {
            return objCapaDato.GetListFoodBrands();
        }

        //-----------------------------------------------------------------------------------------------
        public int CN_AddFoodBrands(FoodBrands obj, out string message)    //    REGISTRAR PRODUCTO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.brandName) || string.IsNullOrWhiteSpace(obj.brandName))
            {
                message = "El nombre del restaurante no puede ser vacio";
            }
            //
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.AddFoodBrands(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return 0;
            }
        }
        //-----------------------------------------------------------------------------------------------
        public bool CN_UpdateFoodBrands(FoodBrands obj, out string message)     //  EDITAR PRODUCTO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.brandName) || string.IsNullOrWhiteSpace(obj.brandName))
            {
                message = "El nombre del restaurante no puede ser vacio";
            }
            //
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.UpdateFoodBrands(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return false;
            }
        }
        //-----------------------------------------------------------------------------------------------
        public bool CN_SaveDataImg(FoodBrands obj, out string message)
        {
            return objCapaDato.SaveDataImg(obj, out message);
        }
        //-----------------------------------------------------------------------------------------------
        //public bool CN_DeleteProduct(int id, out string message)    //     ELIMINAR PRODUCTO
        //{
        //    return objCapaDato.DeleteProduct(id, out message);
        //}
    }
}