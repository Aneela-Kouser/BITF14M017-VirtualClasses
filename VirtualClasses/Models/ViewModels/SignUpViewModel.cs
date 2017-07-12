using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualClasses.Models;

namespace VirtualClasses.Models.ViewModels
{
    public class SignUpViewModel
    {
        public bool doesUsernameExist{ get; set; }
        public bool doesEmailExist { get; set; }
        public Teacher data { get; set; }
    }
}