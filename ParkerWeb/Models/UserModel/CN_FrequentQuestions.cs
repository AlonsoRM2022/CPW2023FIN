using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.UserModel
{
    public class CN_FrequentQuestions
    {
        private CD_FrequentQuestions objCapaDato = new CD_FrequentQuestions();    //  INSTANCIA DEL OBJETO
        public List<FrequentQuestions> CN_GetListFrequentQuestions()
        {
            return objCapaDato.GetListFrequentQuestions();
        }

        public int CN_AddFrequentQuestion(FrequentQuestions obj, out string message)    //    REGISTRAR USUARIO
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.questionTitle) || string.IsNullOrWhiteSpace(obj.questionTitle))
            {
                message = "La pregunta no puede estar vacia";
            }
            else if (string.IsNullOrEmpty(obj.questionBody) || string.IsNullOrWhiteSpace(obj.questionBody))
            {
                message = "La respuesta no puede ser vacia";
            }
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.AddFrequentQuestions(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return 0;
            }
        }

        public bool CN_UpdateFrequentQuestion(FrequentQuestions obj, out string message)
        {
            message = string.Empty;
            //  VALIDACIONES
            if (string.IsNullOrEmpty(obj.questionTitle) || string.IsNullOrWhiteSpace(obj.questionTitle))
            {
                message = "La pregunta no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.questionBody) || string.IsNullOrWhiteSpace(obj.questionBody))
            {
                message = "La respuesta no puede ser vacia";
            }
            if (string.IsNullOrEmpty(message))
            {
                return objCapaDato.UpdateFrequentQuestions(obj, out message); //  RETORNAR OBJETO
            }
            else
            {
                return false;
            }
        }
    }
}