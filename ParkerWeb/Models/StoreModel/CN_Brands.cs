using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.StoreModel
{
    public class CN_Brands
    {
        private CD_Brands objCapaDato = new CD_Brands();    //  INSTANCIA DEL OBJETO
        public List<Brands> CN_GetListBrands()
        {
            return objCapaDato.GetListBrands();
        }

        //
        public int CN_AddBrand(Brands obj, out string message)    //    REGISTRAR MARCA
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.brandName) || string.IsNullOrWhiteSpace(obj.brandName))
            {
                message = "Debe ingresar un nombre. La nombre de la marca no puede ser vacio";
            }
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.AddBrand(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return 0;
            }
        }
        public bool CN_UpdateBrand(Brands obj, out string message)     //  EDITAR MARCA
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.brandName) || string.IsNullOrWhiteSpace(obj.brandName))
            {
                message = "Debe ingresar un nombre. El nombre de la marca no puede ser vacio";
            }
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.UpdateBrand(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return false;
            }
        }
        //
        public bool CN_DeleteBrand(int IDBrand, out string message)    //     ELIMINAR MARCA
        {
            return objCapaDato.DeleteBrand(IDBrand, out message);
        }

        public List<Brands> CN_GetListBrandXCategorie(int idCategory)
        {
            return objCapaDato.GetListBrandXCategorie(idCategory);
        }
    }
}