using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkerWeb.Models.ResourcesModel;

namespace ParkerWeb.Models.UserModel
{
    public class CN_Users
    {
        private CD_Users objCapaDato = new CD_Users();    //  INSTANCIA DEL OBJETO
        public List<Users> CN_GetListUsers()   //  LISTAR USUARIOS
        {
            return objCapaDato.GetListUsers();
        }
        public int CN_AddUser(Users obj, out string message)    //    REGISTRAR USUARIO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.userName) || string.IsNullOrWhiteSpace(obj.userName)) // Validar un formato correcto para el nombre
            {
                message = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.userLastName) || string.IsNullOrWhiteSpace(obj.userLastName)) // Validar un formato correcto para los apellidos
            {
                message = "Los apellidos del usuario no pueden ser vacios";
            }
            else if (string.IsNullOrEmpty(obj.userMail) || string.IsNullOrWhiteSpace(obj.userMail)) // Validar un formato correcto para el correo
            {
                message = "El correo del usuario no puede ser vacio";
            }
            else if (obj.objRol.rolID == 0)
            {
                message = "Debe seleccionar un rol de usuario";
            }
            // Si no hubo errores de validación
            if (string.IsNullOrEmpty(message))
            {
                string psswd = CN_Resources.AutoGenerateKey(); // Se genera una clave automatica
                string subject = "Cuenta de Casa Parker"; // Asunto del correo
                string bodyMail = @"
                                        <!DOCTYPE html>
                                        <html>
                                        <head>
                                            <style>
                                                body { font-family: Arial, sans-serif; line-height: 1.6; color: #333333; }
                                                .container { width: 80%; margin: 0 auto; background-color: #f2f2f2; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }
                                                h3 { color: #4CAF50; }
                                                .footer { font-size: 0.8em; text-align: center; margin-top: 20px; color: #888888; }
                                                .important { color: #333333; font-weight: bold; }
                                            </style>
                                        </head>
                                        <body>
                                            <div class='container'>
                                                <h3>Casa Parker Web</h3>
                                                <p>Sr(a). <span class='important'>!nombre! !apellidos!</span>,<br>
                                                Su cuenta de Casa Parker Web ha sido creada con éxito.</p>
                                                <p>Su clave de acceso es la siguiente: <span class='important'>!clave!</span></p>
                                                <p>Una vez que ingrese al sistema, deberá cambiar la clave por una nueva de su preferencia.</p>
                                                <div class='footer'>
                                                    <p>Gracias por elegirnos,</p>
                                                    <p>Equipo de Casa Parker Web</p>
                                                </div>
                                            </div>
                                        </body>
                                        </html>";
                bodyMail = bodyMail.Replace("!nombre!", obj.userName); // Se remplaza el texto del correo por el nombre
                bodyMail = bodyMail.Replace("!apellidos!", obj.userLastName); // Se remplaza el texto del correo por el apellido
                bodyMail = bodyMail.Replace("!clave!", psswd); // Se remplaza el texto del correo por la clave generada automaticamente
                obj.userPassword = CN_Resources.ConvertToSha256(psswd);

                int userId = objCapaDato.AddUser(obj, out message);
                if (userId > 0)
                {
                    
                    bool response = CN_Resources.SendMail(obj.userMail, subject, bodyMail); // Se envia el correo 
                    if (!response) // Si el correo se envió correctamente
                    {
                        
                    }
                        return userId; //  RETORNAR ID DEL USUARIO REGISTRADO
               
                }
                else
                {
                    // No se pudo registrar
                    return 0;
                }
            }
            else
            {
                // Hubo errores de validación
                return 0;
            }
        }
        public int CN_AddCustomer(Users obj, out string message)
        {
            message = string.Empty;

            // VALIDACIONES
            if (string.IsNullOrEmpty(obj.userName) || string.IsNullOrWhiteSpace(obj.userName))
            {
                message = "El nombre del cliente no puede ser vacío.";
            }
            else if (string.IsNullOrEmpty(obj.userLastName) || string.IsNullOrWhiteSpace(obj.userLastName))
            {
                message = "Los apellidos del cliente no pueden ser vacíos.";
            }
            else if (string.IsNullOrEmpty(obj.userMail) || string.IsNullOrWhiteSpace(obj.userMail))
            {
                message = "El correo del cliente no puede ser vacío.";
            }

            // Si no hubo errores de validación
            if (string.IsNullOrEmpty(message))
            {
                string psswd = CN_Resources.AutoGenerateKey();
                string subject = "Cuenta de Casa Parker";
                string bodyMail = "<h3>Casa Parker Web</h3></br><p>Sr(a). !nombre! !apellidos! </br>Su cuenta de Casa Parker Web ha " +
                    "sido creada con éxito.</br>Su clave de acceso es la siguiente: <strong> !clave! </strong></p>" +
                    "<p>Una vez que ingrese al sistema deberá cambiar la clave por una nueva clave de su preferencia.</p>" +
                    "<p></br> Ahora podrá realizar compras dentro de la tienda y recibir notificaciones de los eventos que se realizarán en nuestro local.</p>";

                bodyMail = bodyMail.Replace("!nombre!", obj.userName);
                bodyMail = bodyMail.Replace("!apellidos!", obj.userLastName);
                bodyMail = bodyMail.Replace("!clave!", psswd);
                obj.userPassword = CN_Resources.ConvertToSha256(psswd);
                // Realizar el registro del cliente
                int userId = objCapaDato.AddCustomer(obj, out message);

                if (userId > 0)
                {
                    // El registro fue exitoso, ahora intenta enviar el correo
                    bool response = CN_Resources.SendMail(obj.userMail, subject, bodyMail);

                    if (!response)
                    {
                        // Si no se puede enviar el correo, puedes manejar la situación aquí
                        message = "No se pudo enviar el correo, pero el cliente se registró exitosamente.";
                    }

                    return userId; // Retornar el ID del cliente registrado
                }
                else
                {
                    // Si hubo un error en el registro, ya se estableció el mensaje de error
                    return 0;
                }
            }
            else
            {
                // Hubo errores de validación
                return 0;
            }
        }

        public bool CN_UpdateUser(Users obj, out string message)     //  EDITAR USUARIO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.userName) || string.IsNullOrWhiteSpace(obj.userName)) // Validar un formato correcto para el nombre
            {
                message = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.userLastName) || string.IsNullOrWhiteSpace(obj.userLastName)) // Validar un formato correcto para los apellidos
            {
                message = "Los apellidos del usuario no pueden ser vacios";
            }
            else if (string.IsNullOrEmpty(obj.userMail) || string.IsNullOrWhiteSpace(obj.userMail)) // Validar un formato correcto para el correo
            {
                message = "El correo del usuario no puede ser vacio";
            }
            if (string.IsNullOrEmpty(message))
            {
                /* 
                 * En caso de pasar las validaciones se edita el usuario
                 */
                return objCapaDato.UpdateUser(obj, out message);
            }
            else
            { // No se pudo editar
                return false;
            }
        }
        //public bool CN_DeleteUser(int userID, out string message)    //     ELIMINAR USUARIO
        //{
        //    return objCapaDato.DeleteUser(userID, out message);
        //}

        public bool CN_ChangePassword(int userID, string password, out string message)    //     CAMBIAR CLAVE DEL USUARIO
        {
            return objCapaDato.ChangePassword(userID, password, out message);
        }

        public bool CN_ResetPassword(int userID, string mailAddress, out string message)    //    RESTABLECER CLAVE DEL USUARIO
        {
            message = string.Empty;
            string newPassword = CN_Resources.AutoGenerateKey();
            bool result = objCapaDato.ResetPassword(userID, CN_Resources.ConvertToSha256(newPassword), out message);
            if (result) // Se restableció la contraseña en la capa de datos
            {
                string subject = "Restablecer Contraseña";
                string bodyMail = "<h3>Su solicitud de restablecer contraseña fue exitosa.</h3></br><p>Su nueva clave de acceso es: !clave! </p>" +
                    "<p>Una vez que ingrese al sistema deberá cambiar la clave por una nueva clave de su preferencia.</p>";
                bodyMail = bodyMail.Replace("!clave!", newPassword);
                bool response = CN_Resources.SendMail(mailAddress, subject, bodyMail);
                if (response) // Si el correo se envió correctamente
                {
                    return true; // Indica que se restableció la contraseña y se envió el correo
                }
                else
                {
                    message = "No se pudo enviar el correo, pero la contraseña se restableció.";
                    return false; // Indica que se restableció la contraseña pero no se pudo enviar el correo
                }
            }
            else
            {
                // No se pudo restablecer la contraseña en la capa de datos
                // El mensaje de error ya se estableció en objCapaDato.ResetPassword()
                return false; // Indica que no se pudo restablecer la contraseña
            }
        }
    }
}