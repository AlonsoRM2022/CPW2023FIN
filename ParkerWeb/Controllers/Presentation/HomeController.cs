using ParkerWeb.Models.BlogModel;
using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkerWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FAQS()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Publication(int idPost = 0)
        {
            Posts objPost = new Posts();
            //bool conversion;
            objPost = new CN_Posts().CN_GetListPosts().Where(p => p.postID == idPost).FirstOrDefault();
            //if (objPost != null)
            //{
            //    objPost.base64 = CN_Resources.ConvertToBase64(Path.Combine(objPost.postImageRoute, objPost.postImageName), out conversion);
            //    objPost.extension = Path.GetExtension(objPost.postImageName);
            //}
            return View(objPost);
        }


        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ListPosts(string publication)
        {
            List<Posts> list = new List<Posts>();
            //bool conversion;

            list = new CN_Posts().CN_GetListPosts().Select(p => new Posts()
            {
                postID = p.postID,
                postTitle = p.postTitle,
                postContent = p.postContent,
                objUser = p.objUser,
                postStatus = p.postStatus,
                postDate = p.postDate
            }).Where(p => p.postStatus == true
            ).ToList();
            var jsonresult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }
        [HttpGet]
        [ValidateInput(false)]
        public JsonResult ListPublications()
        {
            List<Posts> list = new List<Posts>();
            list = new CN_Posts().CN_GetListPosts();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListFAQS()
        {
            List<FrequentQuestions> list = new List<FrequentQuestions>();
            list = new CN_FrequentQuestions().CN_GetListFrequentQuestions().Select(p => new FrequentQuestions()
            {
                questionID = p.questionID,
                questionTitle = p.questionTitle,
                questionBody = p.questionBody,
                questionStatus = p.questionStatus
            }).Where(p => p.questionStatus == true).ToList();
            var jsonresult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }
    }
}