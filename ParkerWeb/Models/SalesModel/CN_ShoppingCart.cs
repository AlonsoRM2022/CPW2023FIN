using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.SalesModel
{
    public class CN_ShoppingCart
    {
        private CD_ShoppingCart objCapaDato = new CD_ShoppingCart();

        public bool ExistCart(int userID, int productID)
        {
            return objCapaDato.ExistCart(userID, productID);
        }

        public bool CartOperation(int userID, int productID, bool add, out string message)
        {
            return objCapaDato.CartOperation(userID, productID, add, out message);
        }

        public int CartQuantity(int userID)
        {
            return objCapaDato.CartQuantity(userID);
        }

        public List<ShoppingCart> GetProductList(int userID)
        {
            return objCapaDato.GetProductList(userID);
        }

        public bool CN_DeleteCart(int userID, int productID)
        {
            return objCapaDato.DeleteCart(userID, productID);
        }
    }
}