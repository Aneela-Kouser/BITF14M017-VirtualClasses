using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualClasses.Models.ViewModels
{
    public class JsonComment
    {
        public bool IsCommentOK { get; set; }
        public string CommentBody { get; set; }
        public string CommentTime { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public bool IsFirstComment { get; set; }
    }
}