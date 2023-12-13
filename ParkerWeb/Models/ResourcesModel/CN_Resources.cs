using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.IO;

namespace ParkerWeb.Models.ResourcesModel
{
    public class CN_Resources
    {
        #region GENERAR CLAVE AUTOMATICA
        public static string AutoGenerateKey() // Genera una clave aleatoria
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }
        #endregion
        #region ENCRIPTACION DE LA CONTRASEÑA EN SHA256
        public static string ConvertToSha256(string txt) // Encriptacion de la clave
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(txt));
                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        #endregion
        #region Enviar Correos
        public static bool SendMail(string userMail, string subject, string message) // Envio de correos electronicos
        {
            bool response = false;
            try
            {
                //  CONFIGURACION DEL CORREO
                MailMessage mail = new MailMessage();
                mail.To.Add(userMail);
                mail.From = new MailAddress("fabiansb98@gmail.com");
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                //  SERVIDOR
                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("fabiansb98@gmail.com", "dnyqzesmrbenllvc"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };
                smtp.Send(mail);
                response = true;

            }
            catch (Exception e)
            {
                response = false;
            }
            return response;
        }
        #endregion
        #region IMAGEN B64
        public static string ConvertToBase64(string route, out bool conversion) // Formato para las imagenes
        {
            string txtBase64 = string.Empty;
            conversion = true;
            try
            {
                byte[] bytes = File.ReadAllBytes(route);
                txtBase64 = Convert.ToBase64String(bytes);
            }
            catch
            {
                conversion = false;
            }
            return txtBase64;
        }
        #endregion
    }
}