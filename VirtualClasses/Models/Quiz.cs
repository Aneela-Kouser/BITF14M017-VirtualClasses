//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VirtualClasses.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Quiz
    {
        public Quiz()
        {
            this.Questions = new HashSet<QuizQuestion>();
            this.Solutions = new HashSet<QuizSolution>();
        }
    
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int TotalMarks { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual ICollection<QuizQuestion> Questions { get; set; }
        public virtual ICollection<QuizSolution> Solutions { get; set; }
    }
}
