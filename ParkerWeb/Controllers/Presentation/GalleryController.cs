using ParkerWeb.Models.GalleryModel;
using ParkerWeb.Models.ResourcesModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkerWeb.Controllers.Presentation
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ArtDetail(int idArt)
        {
            Art objArt = new Art();
            bool conversion;
            objArt = new CN_Art().CN_GetListArt().Where(p => p.artID == idArt).FirstOrDefault();
            if (objArt != null)
            {
                objArt.base64 = CN_Resources.ConvertToBase64(Path.Combine(objArt.ImageRoute, objArt.ImageName), out conversion);
                objArt.extension = Path.GetExtension(objArt.ImageName);
            }
            return View(objArt);
        }

        [HttpGet]
        public JsonResult DetailArt(int id)
        {
            List<Art> list = new List<Art>();
            list = new CN_Art().CN_GetListArt().Select(c => new Art()
            {
                title = c.title,
                artDescription = c.artDescription
            }).Where(c => c.artID == id).ToList();
            var jsonresult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }




        [HttpGet]
        public JsonResult ListCategories()
        {
            List<ArtCategories> list = new List<ArtCategories>();
            list = new CN_ArtCategories().CN_GetListArtCategories().Select(c => new ArtCategories()
            {
                categoryID = c.categoryID,
                categoryDescription = c.categoryDescription,
                categoryStatus = c.categoryStatus
            }).Where(c => c.categoryStatus == true).ToList();
            var jsonresult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }


        [HttpPost]
        public JsonResult ListArt(int idCategory)
        {
            List<Art> list = new List<Art>();
            bool conversion;

            list = new CN_Art().CN_GetListArt().Select(p => new Art()
            {
                artID = p.artID,
                artist = p.artist,
                title = p.title,
                artDescription = p.artDescription,
                objArtCategory = p.objArtCategory,
                ImageRoute = p.ImageRoute,
                base64 = CN_Resources.ConvertToBase64(Path.Combine(p.ImageRoute, p.ImageName), out conversion),
                extension = Path.GetExtension(p.ImageName),
                artStatus = p.artStatus
            }).Where(p => p.objArtCategory.categoryID == (idCategory == 0 ? p.objArtCategory.categoryID : idCategory) && p.artStatus == true).ToList();

            var jsonresult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }
    }
}