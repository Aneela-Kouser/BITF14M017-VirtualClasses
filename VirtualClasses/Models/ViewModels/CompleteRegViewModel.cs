using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualClasses.Models.ViewModels
{
    public class CompleteRegViewModel
    {
        public bool IsPinCorect { get; set; }
        public bool IsUsernameCorrect { get; set; }
        public Student Data { get; set; }
    }
}