using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.UserModel
{
    public class FrequentQuestions
    {
        public int questionID { get; set; }
        public string questionTitle { get; set; }
        public string questionBody { get; set; }
        public bool questionStatus { get; set; }
    }
}