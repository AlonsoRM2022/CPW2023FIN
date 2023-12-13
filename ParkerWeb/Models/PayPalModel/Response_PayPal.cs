using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.PayPalModel
{
    public class Response_PayPal<T>
    {
        public bool Status { get; set; }
        public T Response { get; set; }
    }
}