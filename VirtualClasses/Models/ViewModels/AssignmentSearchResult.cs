using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualClasses.Models.ViewModels
{
    public class AssignmentSearchResult
    {
        public string AssignmentTitle { get; set; }
        public int AssignmentId { get; set; }
        public List<AssignmentQuestionSearch> AssignmentQuestions { get; set; }
    }
}