using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualClasses.Models.ViewModels
{
    public class QuizSearchResult
    {
        public string QuizTitle { get; set; }
        public int QuizId { get; set; }
        public List<QuizQuestionSearch> QuizQuestions { get; set; }
    }
}