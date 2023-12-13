using ParkerWeb.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkerWeb.Models.BlogModel
{
    public class Posts
    {
        public int postID { get; set; }
        public string postTitle { get; set; }
        public string postContent { get; set; }
        public string postImageRoute { get; set; }
        public string postImageName { get; set; }
        public bool postStatus { get; set; }
        public Users objUser { get; set; }
        public DateTime postDate { get; set; }
        public string base64 { get; set; }
        public string extension { get; set; }
    }
}