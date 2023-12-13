using ParkerWeb.Filter;
using ParkerWeb.Models.LocationModel;
using ParkerWeb.Models.PayPalModel;
using ParkerWeb.Models.ResourcesModel;
using ParkerWeb.Models.SalesModel;
using ParkerWeb.Models.StoreModel;
using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParkerWeb.Controllers.Presentation
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }
        //
        public ActionResult ProductDetail(int idProduct = 0)
        {
            Products objProduct = new Products();
            bool conversion;
            objProduct = new CN_Products().CN_GetListProducts().Where(p => p.productID == idProduct).FirstOrDefault();
            if (objProduct != null)
            {
                objProduct.base64 = CN_Resources.ConvertToBase64(Path.Combine(objProduct.productImageRoute, objProduct.productImageName), out conversion);
                objProduct.extension = Path.GetExtension(objProduct.productImageName);
            }
            return View(objProduct);
        }


        [HttpGet]
        public JsonResult ListCategories()
        {
            List<Categories> list = new List<Categories>();
            list = new CN_Categories().CN_GetListCategories().Select(c => new Categories()
            {
                categoryID = c.categoryID,
                categoryName = c.categoryName,
                categoryStatus = c.categoryStatus
            }).Where(c => c.categoryStatus == true).ToList();
            var jsonresult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        //
        [HttpPost]
        public JsonResult ListBrandXCategory(int idCategory)
        {
            List<Brands> list = new List<Brands>();
            list = new CN_Brands().CN_GetListBrandXCategorie(idCategory);
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        //
        [HttpPost]
        public JsonResult ListProducts(int idCategory, int idBrand)
        {
            List<Products> list = new List<Products>();
            bool conversion;

            list = new CN_Products().CN_GetListProducts().Select(p => new Products()
            {
                productID = p.productID,
                productName = p.productName,
                productDescription = p.productDescription,
                objBrand = p.objBrand,
                objCategory = p.objCategory,
                productPrice = p.productPrice,
                productStock = p.productStock,
                productImageRoute = p.productImageRoute,
                base64 = CN_Resources.ConvertToBase64(Path.Combine(p.productImageRoute, p.productImageName), out conversion),
                extension = Path.GetExtension(p.productImageName),
                productStatus = p.productStatus
            }).Where(p => p.objCategory.categoryID == (idCategory == 0 ? p.objCategory.categoryID : idCategory) &&
                     p.objBrand.brandID == (idBrand == 0 ? p.objBrand.brandID : idBrand) &&
                     p.productStock > 0 && p.productStatus == true).ToList();

            var jsonresult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        //
        [HttpPost]
        public JsonResult AddCart(int productID)
        {
            int userID = ((Users)Session["User"]).userID;
            bool exists = new CN_ShoppingCart().ExistCart(userID, productID);
            //bool exists = new CN_ShoppingCart().CN_ExistCart(userID, productID);
            bool result = false;
            string message = string.Empty;
            if (exists)
            {
                message = "Este producto ya existe en el carrito de compras";
            }
            else
            {
                result = new CN_ShoppingCart().CartOperation(userID, productID, true, out message);
            }
            return Json(new { result = result, message = message }, JsonRequestBehavior.AllowGet);
        }

        //
        [HttpGet]
        public JsonResult CartQuantity()
        {
            int userID = ((Users)Session["User"]).userID;
            int quantity = new CN_ShoppingCart().CartQuantity(userID);
            return Json(new { quantity = quantity }, JsonRequestBehavior.AllowGet);
        }

        //
        [HttpPost]
        public JsonResult ListCartProducts()
        {
            int userID = ((Users)Session["User"]).userID;
            List<ShoppingCart> objList = new List<ShoppingCart>();
            bool conversion;
            objList = new CN_ShoppingCart().GetProductList(userID).Select(oc => new ShoppingCart()
            {
                objProduct = new Products()
                {
                    productID = oc.objProduct.productID,
                    productName = oc.objProduct.productName,
                    objBrand = oc.objProduct.objBrand,
                    productPrice = oc.objProduct.productPrice,
                    productImageRoute = oc.objProduct.productImageRoute,
                    base64 = CN_Resources.ConvertToBase64(Path.Combine(oc.objProduct.productImageRoute, oc.objProduct.productImageName), out conversion),
                    extension = Path.GetExtension(oc.objProduct.productImageName)
                },
                scQuantity = oc.scQuantity
            }).ToList();
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }

        //
        [HttpPost]
        public JsonResult OperationCart(int productID, bool add)
        {
            int userID = ((Users)Session["User"]).userID;
            bool result = false;
            string message = string.Empty;
            //result = new CN_ShoppingCart().CartOperation(userID, productID, true, out message);
            result = new CN_ShoppingCart().CartOperation(userID, productID, add, out message);
            return Json(new { result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }

        //
        [HttpPost]
        public JsonResult DeleteCart(int productID)
        {
            int userID = ((Users)Session["User"]).userID;
            bool result = false;
            string message = string.Empty;
            result = new CN_ShoppingCart().CN_DeleteCart(userID, productID);
            return Json(new { result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }

        //
        [HttpPost]
        public JsonResult GetProvinces()
        {
            List<Provinces> objList = new List<Provinces>();
            objList = new CN_Location().GetProvinces();
            return Json(new { list = objList }, JsonRequestBehavior.AllowGet);
        }

        //
        [HttpPost]
        public JsonResult GetCantons(int idProvince)
        {
            List<Cantons> objList = new List<Cantons>();
            objList = new CN_Location().GetCantons(idProvince);
            return Json(new { list = objList }, JsonRequestBehavior.AllowGet);
        }

        //
        [HttpPost]
        public JsonResult GetDistricts(int idCanton)
        {
            List<Districts> objList = new List<Districts>();
            objList = new CN_Location().GetDistricts(idCanton);
            return Json(new { list = objList }, JsonRequestBehavior.AllowGet);
        }

        //
        [ValidarSession]
        [Authorize]
        public ActionResult ShoppingCart()
        {
            return View();
        }

        //
        //[HttpPost]
        //public async Task<JsonResult> ProcessPayment(List<ShoppingCart> objShoppingCart, Sales objSale)
        //{
        //    decimal total = 0;
        //    DataTable saleDetail = new DataTable();
        //    saleDetail.Locale = new CultureInfo("es-CR");
        //    saleDetail.Columns.Add("productID", typeof(string));/**/
        //    saleDetail.Columns.Add("scQuantity", typeof(int));
        //    saleDetail.Columns.Add("total", typeof(decimal));

        //    List<Item> objListItem = new List<Item>();

        //    foreach (ShoppingCart objCart in objShoppingCart)
        //    {
        //        decimal subTotal = Convert.ToDecimal(objCart.scQuantity.ToString()) * objCart.objProduct.productPrice;
        //        total += subTotal;

        //        objListItem.Add(new Item()
        //        {
        //            name = objCart.objProduct.productName,
        //            quantity = objCart.scQuantity.ToString(),
        //            unit_amount = new UnitAmount()
        //            {
        //                currency_code = "USD",
        //                value = objCart.objProduct.productPrice.ToString("G", new CultureInfo("es-CR"))
        //            }
        //        });

        //        saleDetail.Rows.Add(new object[]
        //        {
        //            objCart.objProduct.productID,
        //            objCart.scQuantity,
        //            subTotal
        //        });
        //    }

        //    PurchaseUnit purchaseUnit = new PurchaseUnit()
        //    {
        //        amount = new Amount()
        //        {
        //            currency_code = "USD",
        //            value = total.ToString("G", new CultureInfo("es-CR")),
        //            breakdown = new Breakdown()
        //            {
        //                item_total = new ItemTotal()
        //                {
        //                    currency_code = "USD",
        //                    value = total.ToString("G", new CultureInfo("es-CR")),
        //                }
        //            }
        //        },
        //        description = "Compra de artículos",
        //        items = objListItem
        //    };

        //    Checkout_Order oCheckOutOrder = new Checkout_Order()
        //    {
        //        intent = "CAPTURE",
        //        purchase_units = new List<PurchaseUnit>() { purchaseUnit },
        //        application_context = new ApplicationContext()
        //        {
        //            brand_name = "CasaParker.com",
        //            landing_page = "NO_PREFERENCE",
        //            user_action = "PAY_NOW",
        //            return_url = "https://localhost:44344/Store/PaymentCompleted", // **Revisar
        //            cancel_url = "https://localhost:44344/Store/ShoppingCart"   // **Revisar
        //        }
        //    };

        //    objSale.totalPrice = total;
        //    objSale.userID = ((Users)Session["User"]).userID;
        //    TempData["Sale"] = objSale;
        //    TempData["SaleDetail"] = saleDetail;

        //    CN_PayPal opaypal = new CN_PayPal();
        //    Response_Paypal<Response_Checkout> response_paypal = new Response_Paypal<Response_Checkout>();
        //    response_paypal = await opaypal.CreateRequest(oCheckOutOrder);

        //    return Json(response_paypal, JsonRequestBehavior.AllowGet);
        //}




        [HttpPost]
        public async Task<JsonResult> ProcessPayment(List<ShoppingCart> oListCart, Sales oSale)
        {
            decimal total = 0;
            DataTable saleDetail = new DataTable();
            saleDetail.Locale = new CultureInfo("es-CR");
            saleDetail.Columns.Add("productID", typeof(string));
            saleDetail.Columns.Add("scQuantity", typeof(int));
            saleDetail.Columns.Add("total", typeof(decimal));


            List<Item> oListItem = new List<Item>();


            foreach (ShoppingCart oShoppingCart in oListCart)
            {

                decimal subtotal = Convert.ToDecimal(oShoppingCart.scQuantity.ToString()) * oShoppingCart.objProduct.productPrice;
                total += subtotal;

                oListItem.Add(new Item()
                {
                    name = oShoppingCart.objProduct.productName,
                    quantity = oShoppingCart.scQuantity.ToString(),
                    unit_amount = new UnitAmount()
                    {
                        currency_code = "USD",
                        value = oShoppingCart.objProduct.productPrice.ToString("G", new CultureInfo("es-CR"))
                    }
                });

                saleDetail.Rows.Add(new object[]
                {
                    oShoppingCart.objProduct.productID,
                    oShoppingCart.scQuantity,
                    subtotal
                });
            }

            PurchaseUnit purchaseUnit = new PurchaseUnit()
            {
                amount = new Amount()
                {
                    currency_code = "USD",
                    value = total.ToString("G", new CultureInfo("es-CR")),
                    breakdown = new Breakdown()
                    {
                        item_total = new ItemTotal()
                        {
                            currency_code = "USD",
                            value = total.ToString("G", new CultureInfo("es-CR")),
                        }
                    }
                },
                description = "Compra en la tienda",
                items = oListItem
            };

            Checkout_Order oCheckOutOrder = new Checkout_Order()
            {
                intent = "CAPTURE",
                purchase_units = new List<PurchaseUnit>() { purchaseUnit },
                application_context = new ApplicationContext()
                {
                    brand_name = "CasaParker.com",
                    landing_page = "NO_PREFERENCE",
                    user_action = "PAY_NOW",
                    //return_url = "https://localhost:44341/Store/PaymentCompleted",
                    //cancel_url = "https://localhost:44341/Store/ShoppingCart"
                    return_url = "http://www.parkerweb.somee.com/Store/PaymentCompleted",
                    cancel_url = "http://www.parkerweb.somee.com/Store/ShoppingCart"
                }
            };
            oSale.totalPrice = total;
            oSale.userID = ((Users)Session["User"]).userID;

            TempData["Sale"] = oSale;
            TempData["SaleDetail"] = saleDetail;

            CN_PayPal opaypal = new CN_PayPal();
            Response_PayPal<Response_Checkout> response_paypal = new Response_PayPal<Response_Checkout>();
            response_paypal = await opaypal.CreateRequest(oCheckOutOrder);
            return Json(response_paypal, JsonRequestBehavior.AllowGet);
        }



        public async Task<ActionResult> PaymentCompleted()
        {
            string token = Request.QueryString["token"];
            CN_PayPal opaypal = new CN_PayPal();
            Response_PayPal<Response_Capture> response_paypal = new Response_PayPal<Response_Capture>();
            response_paypal = await opaypal.ApprovePayment(token);

            ViewData["Status"] = response_paypal.Status;

            if (response_paypal.Status)
            {
                Sales oSale = (Sales)TempData["Sale"];
                DataTable saleDetail = (DataTable)TempData["SaleDetail"];
                oSale.saleTransactionID = response_paypal.Response.purchase_units[0].payments.captures[0].id;
                string message = string.Empty;
                bool result = new CN_Sales().AddSale(oSale, saleDetail, out message);
                ViewData["saleTransactionID"] = oSale.saleTransactionID;
            }
            return View();
        }




        //
        //[ValidarSession]
        //[Authorize]
        //public async Task<ActionResult> PaymentCompleted()
        //{
        //    string token = Request.QueryString["token"];
        //    CN_PayPal objPayPal = new CN_PayPal();
        //    Response_Paypal<Response_Capture> response_paypal = new Response_Paypal<Response_Capture>();
        //    response_paypal = await objPayPal.ApprovePayment(token);
        //    ViewData["Status"] = response_paypal.Status;
        //    if (response_paypal.Status)
        //    {
        //        Sales objSales = (Sales)TempData["Sale"];
        //        DataTable SaleDetails = (DataTable)TempData["SaleDetails"];
        //        objSales.saleTransactionID = response_paypal.Response.purchase_units[0].payments.captures[0].id;
        //        string message = string.Empty;
        //        bool result = new CN_Sales().AddSale(objSales, SaleDetails, out message);
        //        ViewData["transactionID"] = objSales.saleTransactionID;
        //    }
        //    return View();
        //}

        //
        [ValidarSession]
        [Authorize]
        public ActionResult Bills()
        {
            int userID = ((Users)Session["User"]).userID;
            List<SalesDetail> objList = new List<SalesDetail>();
            bool conversion;
            objList = new CN_Sales().GetListSales(userID).Select(oc => new SalesDetail()
            {
                objProduct = new Products()
                {
                    productName = oc.objProduct.productName,
                    productPrice = oc.objProduct.productPrice,
                    base64 = CN_Resources.ConvertToBase64(Path.Combine(oc.objProduct.productImageRoute, oc.objProduct.productImageName), out conversion),
                    extension = Path.GetExtension(oc.objProduct.productImageName)
                },
                salesDetailQuantity = oc.salesDetailQuantity,
                total = oc.total,
                saleTransactionID = oc.saleTransactionID
            }).ToList();

            return View(objList);
        }
    }
}