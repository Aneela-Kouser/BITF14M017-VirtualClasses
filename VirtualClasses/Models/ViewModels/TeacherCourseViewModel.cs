using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualClasses.Models.ViewModels
{
    public class TeacherCourseViewModel
    {
        public Course course { get; set; }
        public Quiz quiz { get; set; }
        public Assignment assignment { get; set; }
        public Announcement announcement { get; set; }
        public Discussion discussion { get; set; }
    }
}