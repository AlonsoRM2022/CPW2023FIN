using ParkerWeb.Models.ResourcesModel;
using ParkerWeb.Models.RestaurantModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkerWeb.Controllers.Presentation
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Index()
        {
            return View();
        }
        //
        [HttpPost]
        public JsonResult ListFoods(int idCategory, int idBrand)
        {
            List<Foods> list = new List<Foods>();
            bool conversion;

            list = new CN_Foods().CN_GetListFoods().Select(p => new Foods()
            {
                foodID = p.foodID,
                foodName = p.foodName,
                foodDescription = p.foodDescription,
                objFoodCategory = p.objFoodCategory,
                foodPrice = p.foodPrice,
                foodImageRoute = p.foodImageRoute,
                foodImageName = p.foodImageName,
                base64 = CN_Resources.ConvertToBase64(Path.Combine(p.foodImageRoute, p.foodImageName), out conversion),
                extension = Path.GetExtension(p.foodImageName),
                foodStatus = p.foodStatus
            }).Where(p => p.objFoodCategory.categoryID == (idCategory == 0 ? p.objFoodCategory.categoryID : idCategory) &&
                     p.foodStatus == true).ToList();

            var jsonresult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        [HttpGet]
        public JsonResult ListRestaurants()
        {
            List<FoodBrands> list = new List<FoodBrands>();
            bool conversion;
            list = new CN_FoodBrands().CN_GetListFoodBrands().Select(r => new FoodBrands()
            {
                brandID = r.brandID,
                brandName = r.brandName,
                brandStatus = r.brandStatus,
                brandImageRoute = r.brandImageRoute,
                base64 = CN_Resources.ConvertToBase64(Path.Combine(r.brandImageRoute, r.brandImageName), out conversion),
                extension = Path.GetExtension(r.brandImageName),
            }).Where(r => r.brandStatus == true).ToList();
            var jsonresult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }
        //
        [HttpPost]
        public JsonResult ListCategoryXBrand(int idbrand)
        {
            List<FoodCategories> list = new List<FoodCategories>();
            list = new CN_FoodCategories().CN_GetListCategoryXBrand(idbrand);
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult FoodDetail(int idFood)
        {
            Foods objFoods = new Foods();
            bool conversion;
            objFoods = new CN_Foods().CN_GetListFoods().Where(p => p.foodID == idFood).FirstOrDefault();
            if (objFoods != null)
            {
                objFoods.base64 = CN_Resources.ConvertToBase64(Path.Combine(objFoods.foodImageRoute, objFoods.foodImageName), out conversion);
                objFoods.extension = Path.GetExtension(objFoods.foodImageName);
            }
            return View(objFoods);
        }
    }
}