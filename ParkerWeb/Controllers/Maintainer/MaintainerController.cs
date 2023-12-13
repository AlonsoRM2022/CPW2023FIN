using Newtonsoft.Json;
using ParkerWeb.Models.GalleryModel;
using ParkerWeb.Models.ResourcesModel;
using ParkerWeb.Models.RestaurantModel;
using ParkerWeb.Models.SalesModel;
using ParkerWeb.Models.StoreModel;
using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkerWeb.Controllers.Maintainer
{
    [Authorize]
    public class MaintainerController : Controller
    {
        // GET: Maintainer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StoreCategories()
        {
            return View();
        }
        public ActionResult StoreBrands()
        {
            return View();
        }
        public ActionResult StoreProducts()
        {
            return View();
        }
        public ActionResult Foods()
        {
            return View();
        }
        public ActionResult FoodCategories()
        {
            return View();
        }
        public ActionResult FoodBrands()
        {
            return View();
        }

        public ActionResult Art()
        {
            return View();
        }
        public ActionResult ArtCategories()
        {
            return View();
        }

        public ActionResult FrequentQuestions()
        {
            return View();
        }

        public ActionResult UsersLog()
        {
            return View();
        }

        public ActionResult Errors()
        {
            return View();
        }

        /*+++++++++++++++++++++++   StoreCategories  +++++++++++++++++++++++++*/
        #region CRUD_CATEGORIA
        #region METODO LISTAR
        [HttpGet]
        public JsonResult ListCategories()  //  
        {
            List<Categories> objList = new List<Categories>();
            objList = new CN_Categories().CN_GetListCategories();
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ALMACENAR 
        [HttpPost]
        public JsonResult SaveCategory(Categories category)
        {
            object result;
            string message = string.Empty;
            //  VALIDACION 
            if (category.categoryID == 0) //  EL USUARIO NO EXISTE
            {
                result = new CN_Categories().CN_AddCategorie(category, out message);
            }
            else   // LA CATEGORIA YA EXISTE
            {
                result = new CN_Categories().CN_UpdateCategorie(category, out message);
            }
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ELIMINAR
        [HttpPost]
        public JsonResult RemoveCategorie(int idCategory)
        {
            bool result = false;
            string message = string.Empty;
            result = new CN_Categories().CN_DeleteCategorie(idCategory, out message);
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        /*+++++++++++++++++++++++   StoreCategories De Comidas  +++++++++++++++++++++++++*/
        #region CRUD_FOODCATEGORIA
        #region METODO LISTAR
        [HttpGet]
        public JsonResult ListFoodCategories()  //  
        {
            List<FoodCategories> objList = new List<FoodCategories>();
            objList = new CN_FoodCategories().CN_GetListFoodCategories();
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ALMACENAR 
        [HttpPost]
        public JsonResult SaveFoodCategories(FoodCategories foodCategory)
        {
            object result;
            string message = string.Empty;
            //  VALIDACION 
            if (foodCategory.categoryID == 0) //  EL USUARIO NO EXISTE
            {
                result = new CN_FoodCategories().CN_AddCategorie(foodCategory, out message);
            }
            else   // LA CATEGORIA YA EXISTE
            {
                result = new CN_FoodCategories().CN_UpdateCategorie(foodCategory, out message);
            }
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ELIMINAR
        [HttpPost]
        public JsonResult RemoveFoodCategories(int idCategory)
        {
            bool result = false;
            string message = string.Empty;
            result = new CN_FoodCategories().CN_DeleteCategorie(idCategory, out message);
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        /*+++++++++++++++++++++++   StoreBrands  +++++++++++++++++++++++++*/
        #region CRUD_MARCAS
        #region METODO LISTAR
        [HttpGet]
        public JsonResult ListBrands()  //  METODO LISTAR
        {
            List<Brands> objList = new List<Brands>();
            objList = new CN_Brands().CN_GetListBrands();
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ALMACENAR
        [HttpPost]
        public JsonResult SaveBrand(Brands brand)
        {
            object result;
            string message = string.Empty;
            //  VALIDACION 
            if (brand.brandID == 0) //  LA MARCA NO EXISTE
            {
                result = new CN_Brands().CN_AddBrand(brand, out message);
            }
            else   // LA MARCA YA EXISTE
            {
                result = new CN_Brands().CN_UpdateBrand(brand, out message);
            }
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ELIMINAR
        [HttpPost]
        public JsonResult RemoveBrand(int idBrand)
        {
            bool result = false;
            string message = string.Empty;
            result = new CN_Brands().CN_DeleteBrand(idBrand, out message);
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        /*+++++++++++++++++++++++   StoreProducts  +++++++++++++++++++++++++*/
        #region CRUD_PRODUCTOS
        #region  METODO LISTAR
        [HttpGet]
        public JsonResult ListProducts()  // 
        {
            List<Products> objList = new List<Products>();
            objList = new CN_Products().CN_GetListProducts();
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ALMACENAR PRODUCTO
        [HttpPost]
        public JsonResult SaveProduct(string obj, HttpPostedFileBase fileImg)   //  
        {
            //object result;
            string message = string.Empty;
            bool SuccessOperation = true;
            bool SuccessSaveImg = true;
            Products objProduct = new Products();
            objProduct = JsonConvert.DeserializeObject<Products>(obj);
            decimal priceProduct;
            if (decimal.TryParse(objProduct.productPricTxt, NumberStyles.AllowDecimalPoint, new CultureInfo("es-CR"), out priceProduct))// Modificar productPricTxt por productPriceTxt
            {
                objProduct.productPrice = priceProduct;
            }
            else
            {
                return Json(new { SuccessOperation = false, message = "El formato del precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);
            }
            //  VALIDACION 
            if (objProduct.productID == 0) //  EL PRODUCTO NO EXISTE
            {
                int newProductID = new CN_Products().CN_AddProduct(objProduct, out message);
                if (newProductID != 0)
                {
                    objProduct.productID = newProductID;
                }
                else
                {
                    SuccessOperation = false;
                }
            }
            else   // EL PRODUCTO YA EXISTE
            {
                SuccessOperation = new CN_Products().CN_UpdateProduct(objProduct, out message);
            }
            if (SuccessOperation)
            {
                if (fileImg != null)
                {
                    string imageFolderPath = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(fileImg.FileName);
                    //string ImgName = string.Concat(objProduct.productID.ToString(), extension);
                    string name = "Store_" + objProduct.productID.ToString() + "_" + fileImg.FileName;
                    string ImgName = string.Concat(name, extension);
                    string ImgRoute = Server.MapPath(imageFolderPath);
                    try
                    {
                        fileImg.SaveAs(Path.Combine(ImgRoute, ImgName));
                        SuccessSaveImg = true;
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;
                        SuccessSaveImg = false;
                    }
                    if (SuccessSaveImg)
                    {
                        objProduct.productImageRoute = ImgRoute;
                        objProduct.productImageName = ImgName;
                        bool rspta = new CN_Products().CN_SaveDataImg(objProduct, out message);
                    }
                    else
                    {
                        message = "Se guardo el producto, pero hubo un problema al leer la imagen";
                    }
                }
            }
            return Json(new { SuccessOperation = SuccessOperation, newProductID = objProduct.productID, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ELIMINAR 
        [HttpPost]
        public JsonResult RemoveProduct(int idProduct)
        {
            bool result = false;
            string message = string.Empty;
            result = new CN_Products().CN_DeleteProduct(idProduct, out message);
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region IMAGEN PRODUCTO
        [HttpPost]
        public JsonResult ProductImg(int idProduct)
        {
            bool conversion;
            Products objProduct = new CN_Products().CN_GetListProducts().Where(p => p.productID == idProduct).FirstOrDefault();
            string txtBase64 = CN_Resources.ConvertToBase64(Path.Combine(objProduct.productImageRoute, objProduct.productImageName), out conversion);
            return Json(new
            {
                conversion = conversion,
                txtBase64 = txtBase64,
                extension = Path.GetExtension(objProduct.productImageName)
            },
            JsonRequestBehavior.AllowGet
            );
        }
        #endregion
        #endregion
        /*+++++++++++++++++++++++   Comidas  +++++++++++++++++++++++++*/
        #region CRUD_COMIDAS
        #region  METODO LISTAR
        [HttpGet]
        public JsonResult ListFoods()  // 
        {
            List<Foods> objList = new List<Foods>();
            objList = new CN_Foods().CN_GetListFoods();
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ALMACENAR COMIDAS
        [HttpPost]
        public JsonResult SaveFoods(string obj, HttpPostedFileBase fileImg)   //  
        {
            //object result;
            string message = string.Empty;
            bool SuccessOperation = true;
            bool SuccessSaveImg = true;
            Foods objFood = new Foods();
            objFood = JsonConvert.DeserializeObject<Foods>(obj);
            //  VALIDACION 
            if (objFood.foodID == 0) //  EL PRODUCTO NO EXISTE
            {
                int newFoodID = new CN_Foods().CN_AddFood(objFood, out message);
                if (newFoodID != 0)
                {
                    objFood.foodID = newFoodID;
                }
                else
                {
                    SuccessOperation = false;
                }
            }
            else   // EL PRODUCTO YA EXISTE
            {
                SuccessOperation = new CN_Foods().CN_UpdateFood(objFood, out message);
            }
            if (SuccessOperation)
            {
                if (fileImg != null)
                {
                    string imageFolderPath = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(fileImg.FileName);
                    string name = "Food_" + objFood.foodID.ToString() + "_" + fileImg.FileName;
                    string ImgName = string.Concat(name, extension);
                    string ImgRoute = Server.MapPath(imageFolderPath); // Mover la declaración de ImgRoute fuera del bloque try

                    try
                    {
                        fileImg.SaveAs(Path.Combine(ImgRoute, ImgName));
                        SuccessSaveImg = true; // Marcar éxito
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;
                        SuccessSaveImg = false; // Marcar fallo
                    }

                    if (SuccessSaveImg)
                    {
                        objFood.foodImageRoute = ImgRoute; // Usar ImgRoute aquí
                        objFood.foodImageName = ImgName;
                        bool rspta = new CN_Foods().CN_SaveDataImg(objFood, out message);
                    }
                    else
                    {
                        message = "Se guardó el producto, pero hubo un problema al leer la imagen";
                    }

                }
            }
            return Json(new { SuccessOperation = SuccessOperation, newFoodID = objFood.foodID, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ELIMINAR 
        [HttpPost]
        public JsonResult RemoveFood(int idFood)
        {
            bool result = false;
            string message = string.Empty;
            result = new CN_Foods().CN_DeleteFood(idFood, out message);
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region IMAGEN PRODUCTO
        [HttpPost]
        public JsonResult FoodImg(int idFood)
        {
            bool conversion;
            Foods objFood = new CN_Foods().CN_GetListFoods().Where(f => f.foodID == idFood).FirstOrDefault();
            string txtBase64 = CN_Resources.ConvertToBase64(Path.Combine(objFood.foodImageRoute, objFood.foodImageName), out conversion);
            return Json(new
            {
                conversion = conversion,
                txtBase64 = txtBase64,
                extension = Path.GetExtension(objFood.foodImageName)
            },
            JsonRequestBehavior.AllowGet
            );
        }
        #endregion
        #endregion
        /*+++++++++++++++++++++++   Restaurantes  +++++++++++++++++++++++++*/
        #region CRUD_Restaurantes
        #region  METODO LISTAR
        [HttpGet]
        public JsonResult ListRestaurants()  // 
        {
            List<FoodBrands> objList = new List<FoodBrands>();
            objList = new CN_FoodBrands().CN_GetListFoodBrands();
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ALMACENAR Restaurante
        [HttpPost]
        public JsonResult SaveRestaurant(string obj, HttpPostedFileBase fileImg)   //  
        {
            //object result;
            string message = string.Empty;
            bool SuccessOperation = true;
            bool SuccessSaveImg = true;

            FoodBrands objBrand = new FoodBrands();
            objBrand = JsonConvert.DeserializeObject<FoodBrands>(obj);
            //  VALIDACION 
            if (objBrand.brandID == 0) //  EL PRODUCTO NO EXISTE
            {
                int newBrandID = new CN_FoodBrands().CN_AddFoodBrands(objBrand, out message);
                if (newBrandID != 0)
                {
                    objBrand.brandID = newBrandID;
                }
                else
                {
                    SuccessOperation = false;
                }
            }
            else   // EL PRODUCTO YA EXISTE
            {
                SuccessOperation = new CN_FoodBrands().CN_UpdateFoodBrands(objBrand, out message);
            }
            if (SuccessOperation)
            {
                if (fileImg != null)
                {
                    string imageFolderPath = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(fileImg.FileName);
                    //string ImgName = string.Concat(objBrand.brandID.ToString(), extension);
                    string name = "Restaurant_" + objBrand.brandID.ToString() + "_" + fileImg.FileName;
                    string ImgName = string.Concat(name, extension);
                    string ImgRoute = Server.MapPath(imageFolderPath);
                    try
                    {
                        fileImg.SaveAs(Path.Combine(ImgRoute, ImgName));
                        SuccessSaveImg = true;
                    }
                    catch (Exception e)
                    {

                        string msg = e.Message;
                        SuccessSaveImg = false;
                    }
                    if (SuccessSaveImg)
                    {
                        objBrand.brandImageRoute = ImgRoute;
                        objBrand.brandImageName = ImgName;
                        bool rspta = new CN_FoodBrands().CN_SaveDataImg(objBrand, out message);
                    }
                    else
                    {
                        message = "Se guardo el Restaurante, pero hubo un problema al leer la imagen";
                    }
                }
            }
            return Json(new { SuccessOperation = SuccessOperation, newBrandID = objBrand.brandID, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //#region METODO ELIMINAR 
        //[HttpPost]
        //public JsonResult RemoveRestaurant(int idBrand)
        //{
        //    bool result = false;
        //    string message = string.Empty;
        //    result = new CN_FoodBrands().CN_Delete(idBrand, out message);
        //    return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        //}
        //#endregion
        #region IMAGEN PRODUCTO
        [HttpPost]
        public JsonResult RestaurantImg(int idRestaurant)
        {
            bool conversion;
            FoodBrands objProduct = new CN_FoodBrands().CN_GetListFoodBrands().Where(b => b.brandID == idRestaurant).FirstOrDefault();
            string txtBase64 = CN_Resources.ConvertToBase64(Path.Combine(objProduct.brandImageRoute, objProduct.brandImageName), out conversion);
            return Json(new
            {
                conversion = conversion,
                txtBase64 = txtBase64,
                extension = Path.GetExtension(objProduct.brandImageName)
            },
            JsonRequestBehavior.AllowGet
            );
        }
        #endregion
        #endregion
        /*+++++++++++++++++++++++   StoreCategories De Arte  +++++++++++++++++++++++++*/
        #region CRUD_ARTCATEGORIA
        #region METODO LISTAR
        [HttpGet]
        public JsonResult ListArtCategories()  //  
        {
            List<ArtCategories> objList = new List<ArtCategories>();
            objList = new CN_ArtCategories().CN_GetListArtCategories();
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ALMACENAR 
        [HttpPost]
        public JsonResult SaveArtCategories(ArtCategories ArtCategory)
        {
            object result;
            string message = string.Empty;
            //  VALIDACION 
            if (ArtCategory.categoryID == 0) //  EL USUARIO NO EXISTE
            {
                result = new CN_ArtCategories().CN_AddCategory(ArtCategory, out message);
            }
            else   // LA CATEGORIA YA EXISTE
            {
                result = new CN_ArtCategories().CN_UpdateCategory(ArtCategory, out message);
            }
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        /*+++++++++++++++++++++++  Arte  +++++++++++++++++++++++++*/
        #region CRUD_ARTE
        #region  METODO LISTAR
        [HttpGet]
        public JsonResult ListArt()  // 
        {
            List<Art> objList = new List<Art>();
            objList = new CN_Art().CN_GetListArt();
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region  METODO LISTAR ID
        [HttpGet]
        public JsonResult ListArtDetail(int id)  // 
        {
            List<Art> objList = new List<Art>();
            objList = new CN_Art().GetArtDetail(id);
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ALMACENAR ARTE
        [HttpPost]
        public JsonResult SaveArt(string obj, HttpPostedFileBase fileImg)   //  
        {
            //object result;
            string message = string.Empty;
            bool SuccessOperation = true;
            bool SuccessSaveImg = true;
            Art objArt = new Art();
            objArt = JsonConvert.DeserializeObject<Art>(obj);
            //  VALIDACION 
            if (objArt.artID == 0) //  EL PRODUCTO NO EXISTE
            {
                int newArtID = new CN_Art().CN_AddArt(objArt, out message);
                if (newArtID != 0)
                {
                    objArt.artID = newArtID;
                }
                else
                {
                    SuccessOperation = false;
                }
            }
            else   // EL PRODUCTO YA EXISTE
            {
                SuccessOperation = new CN_Art().CN_UpdateArt(objArt, out message);
            }
            if (SuccessOperation)
            {
                if (fileImg != null)
                {
                    string imageFolderPath = ConfigurationManager.AppSettings["ServidorFotos"];
                    //string extension = Path.GetExtension(fileImg.FileName);
                    string ImgName = "Gallery_" + objArt.artID.ToString() + "_" + fileImg.FileName;
                    //string ImgName = string.Concat(name, extension);
                    string ImgRoute = Server.MapPath(imageFolderPath);
                    //string ImgName = string.Concat(objFood.foodID.ToString(), extension);
                    try
                    {
                        fileImg.SaveAs(Path.Combine(ImgRoute, ImgName));
                        SuccessSaveImg = true;
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;
                        SuccessSaveImg = false;
                    }
                    if (SuccessSaveImg)
                    {
                        objArt.ImageRoute = ImgRoute;
                        objArt.ImageName = ImgName;
                        bool rspta = new CN_Art().CN_SaveDataImg(objArt, out message);
                    }
                    else
                    {
                        message = "Se guarda la obra, pero hubo un problema al leer la imagen";
                    }
                }
            }
            return Json(new { SuccessOperation = SuccessOperation, newArtID = objArt.artID, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region IMAGEN ARTE
        [HttpPost]
        public JsonResult ArtImg(int idArt)
        {
            bool conversion;
            Art objArt = new CN_Art().CN_GetListArt().Where(p => p.artID == idArt).FirstOrDefault();
            string txtBase64 = CN_Resources.ConvertToBase64(Path.Combine(objArt.ImageRoute, objArt.ImageName), out conversion);
            return Json(new
            {
                conversion = conversion,
                txtBase64 = txtBase64,
                extension = Path.GetExtension(objArt.ImageName)
            },
            JsonRequestBehavior.AllowGet
            );
        }
        #endregion
        #endregion
        /*+++++++++++++++++++++++  Preguntas  +++++++++++++++++++++++++*/
        #region CRUD_PREGUNTAFRECUENTE
        #region METODO LISTAR
        [HttpGet]
        public JsonResult ListFrequentQuestions()  //  
        {
            List<FrequentQuestions> objList = new List<FrequentQuestions>();
            objList = new CN_FrequentQuestions().CN_GetListFrequentQuestions();
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ALMACENAR 
        [HttpPost]
        public JsonResult SaveFrequentQuestions(FrequentQuestions FrequentQuestion)
        {
            object result;
            string message = string.Empty;
            //  VALIDACION 
            if (FrequentQuestion.questionID == 0) //  EL USUARIO NO EXISTE
            {
                result = new CN_FrequentQuestions().CN_AddFrequentQuestion(FrequentQuestion, out message);
            }
            else   // LA CATEGORIA YA EXISTE
            {
                result = new CN_FrequentQuestions().CN_UpdateFrequentQuestion(FrequentQuestion, out message);
            }
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion


        /*+++++++++++++++++++++++  User Log  +++++++++++++++++++++++++*/
        #region CRUD_USERLOG
        #region METODO LISTAR
        [HttpGet]
        public JsonResult ListUsersLog()
        {
            List<UsersLog> objList = new List<UsersLog>();
            objList = new CN_UsersLog().GetListUsersLog();
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ALMACENAR 
        [HttpPost]
        public JsonResult SaveUsersLog(UsersLog UsersLog)
        {
            object result;
            string message = string.Empty;
            if (UsersLog.logID == 0) 
            {
                result = new CN_UsersLog().AddUsersLog(UsersLog, out message);
            }
            else  
            {
                result = new CN_UsersLog().AddUsersLog(UsersLog, out message);
            }
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion


        /*+++++++++++++++++++++++  Error  +++++++++++++++++++++++++*/
        #region CRUD_ERROR
        #region METODO LISTAR
        [HttpGet]
        public JsonResult ListErrors()
        {
            List<Errors> objList = new List<Errors>();
            objList = new CN_Errors().GetListErrors();
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region METODO ALMACENAR 
        [HttpPost]
        public JsonResult SaveError(Errors Errors)
        {
            object result;
            string message = string.Empty;
            if (Errors.errorID == 0)
            {
                result = new CN_Errors().AddErrors(Errors, out message);
            }
            else
            {
                result = new CN_Errors().AddErrors(Errors, out message);
            }
            return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion


    }
}