using ParkerWeb.Models.ResourcesModel;
using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ParkerWeb.Controllers.Access
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckIn()
        {
            return View();
        }
        public ActionResult RestorePassword()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        //
        [HttpPost]
        public ActionResult Index(string mail, string psswd)
        {
            Users objUser = new Users();
            objUser = new CN_Users().CN_GetListUsers().Where(item => item.userMail == mail && item.userPassword == CN_Resources.ConvertToSha256(psswd)).FirstOrDefault();
            if (objUser == null)
            {
                ViewBag.Error = "Correo o clave invalidos, por favor verifique sus credenciales.";
                return View();
            }
            else
            {
                if (objUser.userRestore)
                {
                    TempData["userID"] = objUser.userID;
                    return RedirectToAction("ChangePassword", "Access");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(objUser.userMail, false);
                    Session["User"] = objUser;
                    Session["UserRol"] = objUser.objRol.rolName;
                    HttpCookie userCookie = new HttpCookie("UserId", objUser.userID.ToString());
                    userCookie.Expires = DateTime.Now.AddDays(2); // La cookie expira en 2 días
                    userCookie.HttpOnly = true; // Hace que la cookie solo sea accesible a través del servidor HTTP
                    Response.Cookies.Add(userCookie);
                    string userRol = Session["UserRol"] as string;
                    ViewBag.Error = null;
                    if (userRol == "Cliente")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }
        }

        //
        [HttpPost]
        public ActionResult CheckIn(Users obj)
        {
            int result;
            string message = string.Empty;
            ViewData["userName"] = string.IsNullOrEmpty(obj.userName) ? "" : obj.userName;
            ViewData["userLastName"] = string.IsNullOrEmpty(obj.userLastName) ? "" : obj.userLastName;
            ViewData["userMail"] = string.IsNullOrEmpty(obj.userMail) ? "" : obj.userMail;
            result = new CN_Users().CN_AddCustomer(obj, out message);
            if (result > 0)
            {
                ViewBag.SuccessMessage = "Se ha enviado un correo electrónico con la clave de ingreso. Por favor, verifica tu bandeja de entrada.";
                return RedirectToAction("Index", "Access");
            }
            else
            {
                ViewBag.Error = message;
                return View();
            }
        }

        //
        [HttpPost]
        public ActionResult RestorePassword(string mail)
        {
            Users objUser = new Users();
            objUser = new CN_Users().CN_GetListUsers().Where(item => item.userMail == mail).FirstOrDefault();
            if (objUser == null)
            {
                ViewBag.Error = "No se encontro un usuario relacionado a este correo";
                return View();
            }
            string message = string.Empty;
            bool response = new CN_Users().CN_ResetPassword(objUser.userID, mail, out message);
            if (response)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Access");
            }
            else
            {
                ViewBag.Error = message;
                return View();
            }
        }

        //
        [HttpPost]
        public ActionResult ChangePassword(string idUser, string currentPsswd, string newPsswd)
        {
            Users objUser = new Users();
            objUser = new CN_Users().CN_GetListUsers().Where(u => u.userID == int.Parse(idUser)).FirstOrDefault();
            if (objUser.userPassword != CN_Resources.ConvertToSha256(currentPsswd))
            {
                TempData["userID"] = idUser;
                ViewData["userPsswd"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            ViewData["userPsswd"] = "";
            newPsswd = CN_Resources.ConvertToSha256(newPsswd);
            string message = string.Empty;
            bool response = new CN_Users().CN_ChangePassword(int.Parse(idUser), newPsswd, out message);
            if (response)
            {
                return RedirectToAction("Index", "Access");
            }
            else
            {
                TempData["userID"] = idUser;
                ViewBag.Error = message;
                return View();
            }
        }

        //
        public ActionResult SignOff()
        {
            Session["User"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}