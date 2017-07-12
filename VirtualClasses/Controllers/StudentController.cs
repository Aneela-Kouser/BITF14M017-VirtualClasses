using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualClasses.Models.Interfaces;
using VirtualClasses.Models.ViewModels;
using VirtualClasses.Models;
using System.Net;

namespace VirtualClasses.Controllers
{
    public class StudentController : Controller
    {
        IStudentRepository data;
        public StudentController(IStudentRepository s)
        {
            data = s;
        }
        public ActionResult Home()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            Student student = data.GetStudent(loginDetails.Username);
            return View(student);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Home", "Home");
        }
        public new ActionResult Profile()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            Student student = data.GetStudent(loginDetails.Username);
            return View(student);
        }
        public ActionResult ChangePassword()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            Student student = data.GetStudent(loginDetails.Username);
            return View(student);
        }
        public ActionResult ChangePasswordReq(string OldPassword, string Password)
        {
            if (OldPassword == null || OldPassword.Equals("") || Password == null || Password.Equals(""))
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            Student student = data.GetStudent(loginDetails.Username);
            if (!loginDetails.Password.Equals(OldPassword))
            {
                return RedirectToAction("PageNotFoundError", "Home");
            }
            else
            {
                data.ChangePassword(student.Id, Password);
                loginDetails.Password = Password;
                Session["loginDetails"] = loginDetails;
            }
            return RedirectToAction("Home");
        }
        public ActionResult Class(Parameter p)
        {
            if (p.Id == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            bool enr = data.DoesStudentEnrolledInCourse(loginDetails.Username, p.Id);
            if (!enr)
                return RedirectToAction("PageNotFoundError", "Home");
            Course course = data.GetCourse(p.Id);
            if (course == null)
                return RedirectToAction("PageNotFoundError", "Home");

            Student s = data.GetStudent(loginDetails.Username);
            Session["selectedClass"] = p.Id;
            return View(new StudentCourseViewModel { course = course, student = s });
        }
        public ActionResult Assignment(Parameter p)
        {
            if (p.Id == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            object o = Session["selectedClass"];
            if (o == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int id = (int)o;
            Course course = data.GetCourse(id);
            if (course == null)
                return RedirectToAction("PageNotFoundError", "Home");
            Assignment a = data.GetAssignment(p.Id);
            if (a == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int count = (from x in course.Assignments
                         where x.Id == a.Id
                         select x).Count();
            if (count == 0)
                return RedirectToAction("PageNotFoundError", "Home");

            Student s = data.GetStudent(loginDetails.Username);
            return View(new StudentCourseViewModel { course = course, assignment = a, student = s });
        }
        public ActionResult Announcement(Parameter p)
        {
            if (p.Id == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            object o = Session["selectedClass"];
            if (o == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int id = (int)o;
            Course course = data.GetCourse(id);
            if (course == null)
                return RedirectToAction("PageNotFoundError", "Home");

            Announcement a = data.GetAnnouncement(p.Id);
            if (a == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int count = (from x in course.Announcements
                         where x.Id == a.Id
                         select x).Count();
            if (count == 0)
                return RedirectToAction("PageNotFoundError", "Home");

            Student s = data.GetStudent(loginDetails.Username);
            return View(new StudentCourseViewModel { course = course, announcement = a, student = s });
        }
        public ActionResult StartDiscussion()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            object o = Session["selectedClass"];
            if (o == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int id = (int)o;
            Course course = data.GetCourse(id);
            if (course == null)
                return RedirectToAction("PageNotFoundError", "Home");
            if (course.Active == false)
                return RedirectToAction("PageNotFoundError", "Home");
            Student s = data.GetStudent(loginDetails.Username);
            return View(new StudentCourseViewModel { course = course, student = s });
        }
        public ActionResult Discuss(Discussion discussion)
        {
            if (discussion.Title == null || discussion.Body == null)
                return RedirectToAction("StartDiscussion");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            object o = Session["selectedClass"];
            if (o == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int id = (int)o;
            Course course = data.GetCourse(id);
            if (course == null)
                return RedirectToAction("PageNotFoundError", "Home");

            Student s = data.GetStudent(loginDetails.Username);
            discussion.Time = DateTime.Now.ToString();
            discussion.FullName = s.FirstName + " " + s.LastName;
            discussion.Username = s.Username;

            data.SaveDiscussion(id, discussion);
            return RedirectToAction("StartDiscussion");
        }
        public ActionResult Discussion(Parameter p)
        {
            if (p.Id == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            object o = Session["selectedClass"];
            if (o == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int id = (int)o;
            Course course = data.GetCourse(id);
            if (course == null)
                return RedirectToAction("PageNotFoundError", "Home");

            Discussion d = data.GetDiscussion(p.Id);
            if (d == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int count = (from x in course.Discussions
                         where x.Id == d.Id
                         select x).Count();
            if (count == 0)
                return RedirectToAction("PageNotFoundError", "Home");

            Session["discussionSelected"] = p.Id;
            Student s = data.GetStudent(loginDetails.Username);
            return View(new StudentCourseViewModel { course = course, discussion = d, student = s });
        }
        public ActionResult Quiz(Parameter p)
        {
            if (p.Id == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            object o = Session["selectedClass"];
            if (o == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int id = (int)o;
            Course course = data.GetCourse(id);
            if (course == null)
                return RedirectToAction("PageNotFoundError", "Home");
            Quiz q = data.GetQuiz(p.Id);
            if (q == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int count = (from x in course.Quizzes
                         where x.Id == q.Id
                         select x).Count();
            if (count == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            Student s = data.GetStudent(loginDetails.Username);
            if (data.DoesSolutionExist(q.Id, s.Id))
                return RedirectToAction("QuizResult", new { Id = p.Id });

            Session["selectedQuiz"] = q.Id;
            return View(new StudentCourseViewModel { course = course, quiz = q, student = s });
        }
        public ActionResult QuizResult(Parameter p)
        {
            if (p.Id == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            object o = Session["selectedClass"];
            if (o == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int id = (int)o;
            Course course = data.GetCourse(id);
            if (course == null)
                return RedirectToAction("PageNotFoundError", "Home");
            Quiz q = data.GetQuiz(p.Id);
            if (q == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int count = (from x in course.Quizzes
                         where x.Id == q.Id
                         select x).Count();
            if (count == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            Student s = data.GetStudent(loginDetails.Username);
            if (!data.DoesSolutionExist(q.Id, s.Id))
                return RedirectToAction("Quiz", new { Id = p.Id });

            QuizSolution qs = data.GetQuizSolution(q.Id, s.Id);
            if (qs == null)
                return RedirectToAction("PageNotFoundError", "Home");
            return View(new StudentCourseViewModel { course = course, quiz = q, solution = qs, student = s });
        }
        public ActionResult SubmitQuiz()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            object o = Session["selectedClass"];
            if (o == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int courseId = (int)o;
            Course course = data.GetCourse(courseId);
            if (course == null)
                return RedirectToAction("PageNotFoundError", "Home");
            object o1 = Session["selectedQuiz"];
            if (o1 == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int quizId = (int)o1;
            Quiz q = data.GetQuiz(quizId);
            if (q == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int count = (from x in course.Quizzes
                         where x.Id == q.Id
                         select x).Count();
            if (count == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            Student s = data.GetStudent(loginDetails.Username);

            int totalQuestions = int.Parse(Request["TotalQuestions"]);
            List<QuizQuestionSolution> solutions = new List<QuizQuestionSolution>();
            for (int i = 1; i <= totalQuestions; i++)
            {
                QuizQuestionSolution sol = new QuizQuestionSolution();
                sol.QuizQuestionId = int.Parse(Request["QuestionId" + i]);
                if (Request.Form["Answer" + i] != null)
                {
                    sol.SelectedOption = int.Parse(Request.Form["Answer" + i]);
                }
                else
                {
                    sol.SelectedOption = 0;
                }
                solutions.Add(sol);
            }
            data.SaveQuizSolution(q.Id, s.Id, solutions);
            return RedirectToAction("QuizResult", new { Id = q.Id });
        }
        public JsonResult CommentOnDiscussion(string comm)
        {
            JsonComment c = new JsonComment();
            c.IsCommentOK = false;
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return this.Json(c);
            if (Session["discussionSelected"] == null)
                return this.Json(c);
            if (Session["selectedClass"] == null)
                return this.Json(c);
            int user = (int)Session["user"];
            if (user != 2)
                return this.Json(c);
            if (comm == null)
                return this.Json(c);

            int did = (int)Session["discussionSelected"];
            int cid = (int)Session["selectedClass"];
            DiscussionComment comment = new DiscussionComment();
            comment.Body = comm;
            comment.Time = DateTime.Now.ToString();
            Student s = data.GetStudent(loginDetails.Username);
            comment.FullName = s.FirstName + " " + s.LastName;
            comment.Username = s.Username;
            data.AddComment(did, comment);

            c.IsCommentOK = true;
            c.Username = comment.Username;
            c.FullName = comment.FullName;
            c.CommentBody = comment.Body;
            c.CommentTime = comment.Time;
            Discussion d = data.GetDiscussion(did);
            c.IsFirstComment = (d.Comments.Count == 1);
            return this.Json(c);
        }
        public ActionResult CompleteRegistration(string Email)
        {
            if (Email == null)
                return RedirectToAction("PageNotFoundError", "Home");
            if (!data.DoesStudentEmailExist(Email))
                return RedirectToAction("PageNotFoundError", "Home");
            if (data.DoesStudentEmailConfirmed(Email))
                return RedirectToAction("PageNotFoundError", "Home");
            if (Session["completeRegReq"] == null)
            {
                Session.Clear();
                Session["completeRegEmail"] = Email;
                return View();
            }
            else
            {
                CompleteRegViewModel vm = (CompleteRegViewModel)Session["completeRegReq"];
                Session.Clear();
                Session["completeRegEmail"] = Email;
                return View(vm);
            }
        }
        void SendMail(string Email, string Subject, string Body)
        {
            try
            {
                var fromAddress = "bcsf12m039virtualclasses@gmail.com";
                var toAddress = Email;
                const string fromPassword = "*Q1l2m2s33t2*23b2*2hm2d*Muh2mm2d*S3h2r*";
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                }
                smtp.Send(fromAddress, toAddress, Subject, Body);
            }
            catch (Exception) { }
        }
        public ActionResult ResendEmailConfirmation()
        {
            object o = Session["completeRegEmail"];
            if (o == null)
                return RedirectToAction("PageNotFoundError", "Home");
            string Email = (string)o;
            string PIN = data.GetEmailConfirmationPIN(Email);

            string subject = "Confirm Your Email - Virtual Classes";
            string body = "Your Email Confirmation PIN is: " + PIN + "\n";
            body += "\nVirtual Classes\n";
            SendMail(Email, subject, body);
            return RedirectToAction("CompleteRegistration", new { Email = Email });
        }
        public ActionResult CompleteRegistrationReq(Student s)
        {
            if (Session["completeRegEmail"] == null)
                return RedirectToAction("PageNotFoundError", "Home");
            if (s.EmailConfirmationPIN == null || s.FirstName == null || s.LastName == null || s.Username == null || s.Password == null)
                return RedirectToAction("PageNotFoundError", "Home");
            string Email = (string)Session["completeRegEmail"];
            string PIN = data.GetEmailConfirmationPIN(Email);

            CompleteRegViewModel vm = new CompleteRegViewModel();
            vm.IsPinCorect = PIN.Equals(s.EmailConfirmationPIN);
            vm.IsUsernameCorrect = !data.DoesUsernameExist(s.Username);

            if (vm.IsPinCorect && vm.IsUsernameCorrect)
            {
                s.EmailConfirmed = true;
                data.SaveStudentData(Email, s);
                return RedirectToAction("Login", "Home");
            }
            vm.Data = s;
            Session.Clear();
            Session["completeRegReq"] = vm;
            return RedirectToAction("CompleteRegistration", new { Email = Email });
        }
        public ActionResult PageNotFoundError()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 2)
                return RedirectToAction("PageNotFoundError", "Home");
            Student student = data.GetStudent(loginDetails.Username);
            return View(student);
        }
	}
}