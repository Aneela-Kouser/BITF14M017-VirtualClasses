using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualClasses.Models.Interfaces;
using VirtualClasses.Models.ViewModels;

namespace VirtualClasses.Models
{
    public class TeacherRepository : ITeacherRepository
    {
        public Teacher GetTeacher(string username)
        {
            VCDataModelContainer data = new VCDataModelContainer();
            return data.Teachers.First(x => x.Username.Equals(username));
        }
        public Course GetCourse(int cid)
        {
            VCDataModelContainer data = new VCDataModelContainer();
            return data.Courses.Find(cid);
        }
        public void AddCourse(string username, string title)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Course c = new Course();
                c.Title = title;
                c.Active = true;
                data.Teachers.First(x => x.Username.Equals(username)).Courses.Add(c);
                data.SaveChanges();
            }
        }
        public bool DoesUsernameExist(string username)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Teachers
                             where x.Username.Equals(username)
                             select x).Count();
                if (count != 0)
                    return true;
                count = (from x in data.Students
                         where x.Username.Equals(username)
                         select x).Count();
                return count != 0;
            }
        }
        public bool DoesEmailExist(string email)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {

                int count = (from x in data.Teachers
                             where x.Email.Equals(email)
                             select x).Count();
                if (count != 0)
                    return true;
                count = (from x in data.Students
                         where x.Email.Equals(email)
                         select x).Count();
                return count != 0;
            }
        }
        public void EnrollStudent(string Email, int cid)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Course c = data.Courses.Find(cid);
                int count = (from x in c.Students
                             where x.Email.Equals(Email)
                             select x).Count();
                if (count == 0)
                {
                    c.Students.Add(data.Students.First(x => x.Email.Equals(Email)));
                    data.SaveChanges();
                }
            }
        }
        public void AddStudentByEmail(string Email)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Student s = new Student { Email = Email, EmailConfirmed = false };
                data.Students.Add(s);
                data.SaveChanges();
            }
        }

        public Teacher GetInstructor(int cid)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Course c = data.Courses.Find(cid);
                if (c == null)
                    return null;
                return c.Teacher;
            }
        }
        public bool HasConfirmedEmail(string Email)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                         where x.Email.Equals(Email)
                         select x).Count();
                if (count != 0)
                    return data.Students.First(x => x.Email.Equals(Email)).EmailConfirmed;
                return false;
            }
        }
        public void SaveEmailConfirmationPIN(string Email, string PIN)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Email.Equals(Email)
                             select x).Count();
                if (count != 0)
                {
                    data.Students.First(x => x.Email.Equals(Email)).EmailConfirmationPIN = PIN;
                    data.SaveChanges();
                }
            }
        }
        public bool IsTeacherEmail(string Email)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Teachers
                             where x.Email.Equals(Email)
                             select x).Count();
                return count != 0;
            }
        }

        public void SetActive(int id, bool active)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Course c = data.Courses.Find(id);
                if (c != null)
                {
                    c.Active = active;
                    data.SaveChanges();
                }
            }
        }

        public void AddQiuz(Course course, Quiz quiz, List<QuizQuestion> questions)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Course c = data.Courses.Find(course.Id);
                c.Quizzes.Add(quiz);
                data.SaveChanges();
                foreach (QuizQuestion q in questions)
                {
                    quiz.Questions.Add(q);
                }
                data.SaveChanges();
            }
        }
        public void AddAssignment(Course course, Assignment assignment, List<AssignmentQuestion> questions)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Course c = data.Courses.Find(course.Id);
                c.Assignments.Add(assignment);
                data.SaveChanges();
                foreach (AssignmentQuestion q in questions)
                {
                    assignment.Questions.Add(q);
                }
                data.SaveChanges();
            }
        }
        public Quiz GetQuiz(int id)
        {
            VCDataModelContainer data = new VCDataModelContainer();
            Quiz q = data.Quizs.Find(id);
            return q;
        }
        public Assignment GetAssignment(int id)
        {
            VCDataModelContainer data = new VCDataModelContainer();
            Assignment a = data.Assignments.Find(id);
            return a;
        }
        public Announcement GetAnnouncement(int id)
        {
            VCDataModelContainer data = new VCDataModelContainer();
            Announcement a = data.Announcements.Find(id);
            return a;
        }
        public void MakeAnnouncement(int cid, Announcement announcement)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Course c = data.Courses.Find(cid);
                c.Announcements.Add(announcement);
                data.SaveChanges();
            }
        }
        public void SaveDiscussion(int cid, Discussion discussion)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Course c = data.Courses.Find(cid);
                c.Discussions.Add(discussion);
                data.SaveChanges();
            }
        }
        public Discussion GetDiscussion(int id)
        {
            VCDataModelContainer data = new VCDataModelContainer();
            return data.Discussions.Find(id);
        }
        public void AddComment(int did, DiscussionComment comment)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Discussion d = data.Discussions.Find(did);
                d.Comments.Add(comment);
                data.SaveChanges();
            }
        }
        public void ChangePassword(int tId, string Password)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Teacher t = data.Teachers.Find(tId);
                t.Password = Password;
                data.SaveChanges();
            }
        }
        public SearchResults Search(string searchQuery, int classId)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                searchQuery = searchQuery.ToLower();
                SearchResults results = new SearchResults();
                Course course = data.Courses.Find(classId);
                if (course == null)
                    return null;
                List<QuizSearchResult> quizResults = new List<QuizSearchResult>();
                foreach (Quiz quiz in course.Quizzes)
                {
                    QuizSearchResult quizSearchResult = new QuizSearchResult();
                    quizSearchResult.QuizTitle = quiz.Title;
                    quizSearchResult.QuizId = quiz.Id;
                    List<QuizQuestion> questions = (from x in quiz.Questions
                                                    where x.Question.ToLower().Contains(searchQuery) || x.OptionA.ToLower().Contains(searchQuery) || x.OptionB.ToLower().Contains(searchQuery) || x.OptionC.ToLower().Contains(searchQuery) || x.OptionD.ToLower().Contains(searchQuery)
                                                    select x).ToList();
                    List<QuizQuestionSearch> questionSearch = new List<QuizQuestionSearch>();
                    foreach (QuizQuestion ques in questions)
                    {
                        QuizQuestionSearch q = new QuizQuestionSearch();
                        q.Question = ques.Question;
                        q.OptionA = ques.OptionA;
                        q.OptionB = ques.OptionB;
                        q.OptionC = ques.OptionC;
                        q.OptionD = ques.OptionD;
                        questionSearch.Add(q);
                    }
                    quizSearchResult.QuizQuestions = questionSearch;
                    if (questionSearch.Count != 0)
                        quizResults.Add(quizSearchResult);
                }
                results.QuizResults = quizResults;
                List<AssignmentSearchResult> assignmentResults = new List<AssignmentSearchResult>();
                foreach (Assignment assignment in course.Assignments)
                {
                    AssignmentSearchResult assignmentResult = new AssignmentSearchResult();
                    assignmentResult.AssignmentId = assignment.Id;
                    assignmentResult.AssignmentTitle = assignment.Title;
                    List<AssignmentQuestion> questions = (from x in assignment.Questions
                                                          where x.Question.ToLower().Contains(searchQuery)
                                                          select x).ToList();
                    List<AssignmentQuestionSearch> assignmentSearch = new List<AssignmentQuestionSearch>();
                    foreach (AssignmentQuestion q in questions)
                    {
                        AssignmentQuestionSearch q1 = new AssignmentQuestionSearch();
                        q1.Question = q.Question;
                        assignmentSearch.Add(q1);
                    }
                    assignmentResult.AssignmentQuestions = assignmentSearch;
                    if (assignmentSearch.Count != 0)
                        assignmentResults.Add(assignmentResult);
                }
                results.AssignmentResuts = assignmentResults;
                List<Announcement> announcements = (from x in course.Announcements
                                                    where x.Title.ToLower().Contains(searchQuery) || x.Body.ToLower().Contains(searchQuery)
                                                    select x).ToList();
                List<AnnouncementSearch> announcementSearch = new List<AnnouncementSearch>();
                foreach (Announcement ann in announcements)
                {
                    AnnouncementSearch a = new AnnouncementSearch();
                    a.Body = ann.Body;
                    a.Id = ann.Id;
                    a.Title = ann.Title;
                    announcementSearch.Add(a);
                }
                results.Announcements = announcementSearch;

                List<Discussion> discussions = (from x in course.Discussions
                                                where x.Body.ToLower().Contains(searchQuery) || x.Title.ToLower().Contains(searchQuery)
                                                select x).ToList();
                List<DiscussionSearch> discussionSearch = new List<DiscussionSearch>();
                foreach (Discussion dis in discussions)
                {
                    DiscussionSearch d = new DiscussionSearch();
                    d.Body = dis.Body;
                    d.Id = dis.Id;
                    d.Title = dis.Title;
                    discussionSearch.Add(d);
                }
                results.Discussions = discussionSearch;
                return results;
            }
        }
    }
}