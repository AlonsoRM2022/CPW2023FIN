using HtmlAgilityPack;
using Newtonsoft.Json;
using ParkerWeb.Models.BlogModel;
using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace ParkerWeb.Controllers.Maintainer
{
    [Authorize]
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
        //
        [HttpGet]
        public JsonResult ListPosts()
        {
            List<Posts> objList = new List<Posts>();
            objList = new CN_Posts().CN_GetListPosts(); // Almacena la lista de usuario
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet); //Retorna un JSON con la lista
        }

        //
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SavePost(string publication, HttpPostedFileBase fileImg)   //  METODO ALMACENAR 
        {
            //object result;
            string message = string.Empty;
            bool successfulOperation = true;
            bool SuccessSaveImg = true;
            Posts objPost = new Posts();
            objPost = JsonConvert.DeserializeObject<Posts>(publication);
            string userId = userLogged();
            if (objPost != null)
            {
                if (objPost.objUser == null)
                {
                    objPost.objUser = new Users(); // Reemplaza 'User' con el nombre de tu clase de usuario
                }

                int parsedUserId;
                if (int.TryParse(userId, out parsedUserId))
                {
                    objPost.objUser.userID = parsedUserId;
                }
                else
                {
                    // Manejar el caso donde userId no se puede convertir en un entero
                }
            }
            else
            {
                // Manejar el caso en que no se encuentre el ID del usuario
            }

            // Extraer las URLs de las imágenes del contenido HTML utilizando HtmlAgilityPack
            //objPost.PostImages = ExtractImagesFromHtml(objPost.postContent);
            //  VALIDACION 
            if (objPost.postID == 0) //  EL Post NO EXISTE
            {
                int idPostGenerate = new CN_Posts().CN_AddPost(objPost, out message);
                if (idPostGenerate != 0)
                {
                    objPost.postID = idPostGenerate;
                }
                else
                {
                    successfulOperation = false;
                }
            }
            else   // EL USUARIO EXISTE
            {
                successfulOperation = new CN_Posts().CN_UpdatePost(objPost, out message);
            }
            if (successfulOperation)
            {
                if (fileImg != null)
                {
                    string ImgRoute = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(fileImg.FileName);
                    //string ImgName = string.Concat(objPost.postID.ToString(), extension);
                    string name = objPost.postID.ToString() + "Store" + fileImg.FileName;
                    string ImgName = string.Concat(name, extension);
                    try
                    {
                        fileImg.SaveAs(Path.Combine(ImgRoute, ImgName));
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;
                        SuccessSaveImg = false;
                    }
                    if (SuccessSaveImg)
                    {
                        objPost.postImageRoute = ImgRoute;
                        objPost.postImageName = ImgName;
                        bool rspta = new CN_Posts().CN_SaveDataImg(objPost, out message);
                    }
                    else
                    {
                        message = "Se guardo el post, pero hubo un problema al leer la imagen";
                    }
                }
            }
            return Json(new { successfulOperation = successfulOperation, idPostGenerate = objPost.postID, Message = message }, JsonRequestBehavior.AllowGet);
        }

        // Método para extraer las URLs de las imágenes del contenido HTML
        private List<string> ExtractImagesFromHtml(string htmlContent)
        {
            List<string> images = new List<string>();
            if (!string.IsNullOrEmpty(htmlContent))
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);
                // Buscar todas las etiquetas <img> en el contenido HTML
                foreach (HtmlNode imgNode in doc.DocumentNode.SelectNodes("//img"))
                {
                    string src = imgNode.GetAttributeValue("src", "");
                    images.Add(src);
                }
            }
            return images;
        }

        //#region IMAGEN POST
        //[HttpPost]
        //public JsonResult PostImg(int idPost)
        //{
        //    bool conversion;
        //    Posts objPost = new CN_Posts().CN_GetListPosts().Where(p => p.postID == idPost).FirstOrDefault();
        //    string txtBase64 = CN_Resources.ConvertToBase64(Path.Combine(objPost.postImageRoute, objPost.postImageName), out conversion);
        //    return Json(new
        //    {
        //        conversion = conversion,
        //        txtBase64 = txtBase64,
        //        extension = Path.GetExtension(objPost.postImageName)
        //    },
        //    JsonRequestBehavior.AllowGet
        //    );
        //}
        //#endregion

        private string userLogged()
        {
            HttpCookie authCookie = HttpContext.Request.Cookies.Get("UserId"); // Asegúrate de que el nombre de la cookie coincida con el que estableciste
            if (authCookie != null)
            {
                return authCookie.Value;
            }

            return null;
        }

        [HttpPost]
        public JsonResult Delete(int id) 
        {
            bool response = false;
            string message = string.Empty;

            response = new CN_Posts().Delete(id, out message);
            return Json(new { response = response, message = message}, JsonRequestBehavior.AllowGet);
        }
    }
}