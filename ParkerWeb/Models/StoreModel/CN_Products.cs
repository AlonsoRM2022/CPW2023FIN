using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.StoreModel
{
    public class CN_Products
    {
        private CD_Products objCapaDato = new CD_Products();    //  INSTANCIA DEL OBJETO
        public List<Products> CN_GetListProducts()
        {
            return objCapaDato.GetListProducts();
        }

        //-----------------------------------------------------------------------------------------------
        public int CN_AddProduct(Products obj, out string message)    //    REGISTRAR PRODUCTO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.productName) || string.IsNullOrWhiteSpace(obj.productName))
            {
                message = "El nombre del producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.productDescription) || string.IsNullOrWhiteSpace(obj.productDescription))
            {
                message = "La descripcion del producto no puede ser vacio";
            }
            else if (obj.objBrand.brandID == 0)
            {
                message = "Debe seleccionar una marca";
            }
            else if (obj.objCategory.categoryID == 0)
            {
                message = "Debe seleccionar una categoria";
            }
            else if (obj.productPrice == 0)
            {
                message = "El producto debe tener un precio";
            }
            else if (obj.productStock == 0)
            {
                message = "Debe ingresar la cantidad de productos";
            }
            //
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.AddProduct(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return 0;
            }
        }
        //-----------------------------------------------------------------------------------------------
        public bool CN_UpdateProduct(Products obj, out string message)     //  EDITAR PRODUCTO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.productName) || string.IsNullOrWhiteSpace(obj.productName))
            {
                message = "El nombre del producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.productDescription) || string.IsNullOrWhiteSpace(obj.productDescription))
            {
                message = "La descripcion del producto no puede ser vacio";
            }
            else if (obj.objBrand.brandID == 0)
            {
                message = "Debe seleccionar una marca";
            }
            else if (obj.objCategory.categoryID == 0)
            {
                message = "Debe seleccionar una categoria";
            }
            else if (obj.productPrice == 0)
            {
                message = "El producto debe tener un precio";
            }
            else if (obj.productStock == 0)
            {
                message = "Debe ingresar la cantidad de productos";
            }
            //
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.UpdateProduct(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return false;
            }
        }
        //-----------------------------------------------------------------------------------------------
        public bool CN_SaveDataImg(Products obj, out string message)
        {
            return objCapaDato.SaveDataImg(obj, out message);
        }
        //-----------------------------------------------------------------------------------------------
        public bool CN_DeleteProduct(int id, out string message)    //     ELIMINAR PRODUCTO
        {
            return objCapaDato.DeleteProduct(id, out message);
        }
    }
}