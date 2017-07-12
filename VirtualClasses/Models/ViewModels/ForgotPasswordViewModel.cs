using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualClasses.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        public string Message
        {
            get;
            set;
        }
        public bool IsMessageDanger
        {
            get;
            set;
        }
    }
}