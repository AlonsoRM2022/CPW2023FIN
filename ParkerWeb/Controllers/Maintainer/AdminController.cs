using ClosedXML.Excel;
using Newtonsoft.Json;
using ParkerWeb.Models.SalesModel;
using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkerWeb.Controllers.Maintainer
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SalesReport()
        {
            return View();
        }
        public ActionResult UsersControl()  // VISTA USUARIOS
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListRols()  //  METODO LISTAR
        {
            List<Roles> objList = new List<Roles>();
            objList = new CN_Roles().CN_GetListRoles(); // Almacena la lista de Roles
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet); //Retorna un JSON con la lista
        }

        //
        [HttpGet]
        public JsonResult ListUsers()  //  METODO LISTAR
        {
            List<Users> objList = new List<Users>();
            objList = new CN_Users().CN_GetListUsers(); // Almacena la lista de usuario
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet); //Retorna un JSON con la lista
        }
        //
        [HttpPost]
        public JsonResult SaveUser(string user)   //  METODO ALMACENAR USUARIO
        {
            //object result;
            string message = string.Empty;
            bool successfulOperation = true;
            Users objUser = new Users();
            objUser = JsonConvert.DeserializeObject<Users>(user);
            //  VALIDACION 
            if (objUser.userID == 0) //  EL USUARIO NO EXISTE
            {
                int idUserGenerate = new CN_Users().CN_AddUser(objUser, out message);
                if (idUserGenerate != 0)
                {
                    objUser.userID = idUserGenerate;
                }
                else
                {
                    successfulOperation = false;
                }
                //result = new CN_Users().CN_AddUser(objUser, out message);
            }
            else   // EL USUARIO EXISTE
            {
                successfulOperation = new CN_Users().CN_UpdateUser(objUser, out message);
            }
            return Json(new { successfulOperation = successfulOperation, idUserGenerate = objUser.userID, Message = message }, JsonRequestBehavior.AllowGet);
        }
        //
        //[HttpPost]
        //public JsonResult RemoveUser(int idUser)
        //{
        //    bool result = false;
        //    string message = string.Empty;
        //    result = new CN_Users().CN_DeleteUser(idUser, out message);
        //    return Json(new { Result = result, Message = message }, JsonRequestBehavior.AllowGet);
        //}
        //
        [HttpGet]
        public JsonResult ReportList(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reports> objList = new List<Reports>();

            objList = new CN_Reports().Sales(fechainicio,fechafin,idtransaccion);
            return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
        }
        //
        [HttpGet]
        public JsonResult ItemsDashboard()
        {
            Dashboard obj = new CN_Reports().CN_GetListDashboard();
            return Json(new { Result = obj }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportSale(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reports> objList = new List<Reports>();
            objList = new CN_Reports().Sales(fechainicio, fechafin, idtransaccion);
            DataTable dt = new DataTable();
            dt.Locale = new System.Globalization.CultureInfo("es-CR");
            dt.Columns.Add("Fecha Venta", typeof(string));  
            dt.Columns.Add("Cliente", typeof(string));      
            dt.Columns.Add("Producto", typeof(string));     
            dt.Columns.Add("Precio", typeof(decimal));      
            dt.Columns.Add("Cantidad", typeof(int));        
            dt.Columns.Add("Total", typeof(decimal));       
            dt.Columns.Add("IdTransaccion", typeof(string));
            foreach (Reports rp in objList)
            {
                dt.Rows.Add(new object[]
                {
                    rp.FechaVenta,
                    rp.Cliente,
                    rp.Producto,
                    rp.productPrice,
                    rp.salesDetailQuantity,
                    rp.total,
                    rp.saleTransactionID
                });
            }
            dt.TableName = "Datos";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta" + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }
    }
}