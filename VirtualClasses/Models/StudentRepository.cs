using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualClasses.Models.Interfaces;

namespace VirtualClasses.Models
{
    public class StudentRepository : IStudentRepository
    {
        public bool DoesStudentEmailExist(string Email)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Email.Equals(Email)
                             select x).Count();
                return count != 0;
            }
        }
        public bool DoesStudentEmailConfirmed(string Email)
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
        public string GetEmailConfirmationPIN(string Email)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Email.Equals(Email)
                             select x).Count();
                if (count != 0)
                    return data.Students.First(x => x.Email.Equals(Email)).EmailConfirmationPIN;
                return null;
            }
        }
        public bool DoesUsernameExist(string username)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Username.Equals(username)
                             select x).Count();
                if (count != 0)
                    return true;
                count = (from x in data.Teachers
                         where x.Username.Equals(username)
                         select x).Count();
                return count != 0;
            }
        }
        public void SaveStudentData(string Email, Student s)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Email.Equals(Email)
                             select x).Count();
                if (count != 0)
                {
                    Student s1 = data.Students.First(x => x.Email.Equals(Email));
                    s1.EmailConfirmationPIN = "";
                    s1.EmailConfirmed = s.EmailConfirmed;
                    s1.FirstName = s.FirstName;
                    s1.Gender = s.Gender;
                    s1.LastName = s.LastName;
                    s1.Mobile = s.Mobile;
                    s1.Password = s.Password;
                    s1.Username = s.Username;
                    s1.Website = s.Website;
                    data.SaveChanges();
                }
            }
        }
        public Student GetStudent(string username)
        {
            VCDataModelContainer data = new VCDataModelContainer();
            return data.Students.First(x => x.Username.Equals(username));
        }
        public Course GetCourse(int cid)
        {
            VCDataModelContainer data = new VCDataModelContainer();
            return data.Courses.Find(cid);
        }
        public bool DoesStudentEnrolledInCourse(string username, int cid)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Course c = data.Courses.Find(cid);
                if (c == null)
                    return false;
                int count = (from x in c.Students
                             where x.Username != null && x.Username.Equals(username)
                             select x).Count();
                return count != 0;
            }
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
        public Quiz GetQuiz(int id)
        {
            VCDataModelContainer data = new VCDataModelContainer();
            Quiz q = data.Quizs.Find(id);
            return q;
        }
        public bool DoesSolutionExist(int quizId, int studentId)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.QuizSolutions
                             where x.StudentId == studentId && x.QuizId == quizId
                             select x).Count();
                return count != 0;
            }
        }
        public void SaveQuizSolution(int quizId, int studentId, List<QuizQuestionSolution> solutions)
        {
            if (!DoesSolutionExist(quizId, studentId))
            {
                using (VCDataModelContainer data = new VCDataModelContainer())
                {
                    int correct = 0, attempted = 0, marks = 0;
                    for (int i = 0; i < solutions.Count; i++)
                    {
                        QuizQuestion ques = data.QuizQuestions.Find(solutions[i].QuizQuestionId);
                        if (solutions[i].SelectedOption != 0)
                            attempted++;
                        solutions[i].IsCorrect = (ques.CorrectOption == solutions[i].SelectedOption);
                        if (solutions[i].IsCorrect)
                        {
                            correct++;
                            marks += ques.Marks;
                        }
                    }
                    QuizSolution solution = new QuizSolution();
                    solution.StudentId = studentId;
                    solution.QuizId = quizId;
                    solution.Correct = correct;
                    solution.Attempted = attempted;
                    solution.Marks = marks;
                    data.QuizSolutions.Add(solution);
                    data.SaveChanges();
                    foreach (var s in solutions)
                    {
                        solution.QuestionSolutions.Add(s);
                    }
                    data.SaveChanges();
                }
            }
        }
        public QuizSolution GetQuizSolution(int qId, int sId)
        {
            VCDataModelContainer data = new VCDataModelContainer();
            int count = (from x in data.QuizSolutions
                         where x.StudentId == sId && x.QuizId == qId
                         select x).Count();
            if (count != 0)
            {
                QuizSolution qs = (from x in data.QuizSolutions
                                   where x.StudentId == sId && x.QuizId == qId
                                   select x).First();
                return qs;
            }
            return null;
        }
        public void ChangePassword(int sId, string Password)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Student s = data.Students.Find(sId);
                s.Password = Password;
                data.SaveChanges();
            }
        }
    }
}