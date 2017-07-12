using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualClasses.Models.ViewModels
{
    public class SearchResults
    {
        public List<QuizSearchResult> QuizResults { get; set; }
        public List<AssignmentSearchResult> AssignmentResuts { get; set; }
        public List<AnnouncementSearch> Announcements { get; set; }
        public List<DiscussionSearch> Discussions { get; set; }
    }
}