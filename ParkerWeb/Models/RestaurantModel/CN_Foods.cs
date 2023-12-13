using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.RestaurantModel
{
    public class CN_Foods
    {
        private CD_Foods objCapaDato = new CD_Foods();    //  INSTANCIA DEL OBJETO
        public List<Foods> CN_GetListFoods()
        {
            return objCapaDato.GetListFoods();
        }

        //-----------------------------------------------------------------------------------------------
        public int CN_AddFood(Foods obj, out string message)    //    REGISTRAR FoodO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.foodName) || string.IsNullOrWhiteSpace(obj.foodName))
            {
                message = "El nombre de la comida no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.foodDescription) || string.IsNullOrWhiteSpace(obj.foodDescription))
            {
                message = "La descripcion de la comida no puede ser vacio";
            }
            else if (obj.objFoodCategory.categoryID == 0)
            {
                message = "Debe seleccionar una categoria";
            }
            else if (obj.objFoodBrands.brandID == 0)
            {
                message = "Debe seleccionar un Restaurante";
            }
            else if (obj.foodPrice == 0)
            {
                message = "La comida debe tener un precio";
            }
            //
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.AddFood(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return 0;
            }
        }
        //-----------------------------------------------------------------------------------------------
        public bool CN_UpdateFood(Foods obj, out string message)     //  EDITAR FoodO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.foodName) || string.IsNullOrWhiteSpace(obj.foodName))
            {
                message = "El nombre de la comida no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.foodDescription) || string.IsNullOrWhiteSpace(obj.foodDescription))
            {
                message = "La descripcion del comida no puede ser vacio";
            }
            else if (obj.objFoodCategory.categoryID == 0)
            {
                message = "Debe seleccionar una categoria";
            }
            else if (obj.objFoodBrands.brandID == 0)
            {
                message = "Debe seleccionar un Propietario";
            }
            else if (obj.foodPrice == 0)
            {
                message = "La comida debe tener un precio";
            }
            //
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.UpdateFood(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return false;
            }
        }
        //-----------------------------------------------------------------------------------------------
        public bool CN_SaveDataImg(Foods obj, out string message)
        {
            return objCapaDato.SaveDataImg(obj, out message);
        }
        //-----------------------------------------------------------------------------------------------
        public bool CN_DeleteFood(int id, out string message)    //     ELIMINAR FoodO
        {
            return objCapaDato.DeleteFood(id, out message);
        }
    }
}