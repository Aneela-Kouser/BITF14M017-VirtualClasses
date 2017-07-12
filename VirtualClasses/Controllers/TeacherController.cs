using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualClasses.Models;
using VirtualClasses.Models.ViewModels;
using VirtualClasses.Models.Interfaces;
using System.Net;
using System.IO;

namespace VirtualClasses.Controllers
{
    public class TeacherController : Controller
    {
        ITeacherRepository data;
        const string link = "http://bitf14m017virtualclasses.apphb.com/";
        public TeacherController(ITeacherRepository t)
        {
            data = t;
        }
        public ActionResult Home()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            Teacher teacher = data.GetTeacher(loginDetails.Username);
            return View(teacher);
        }
        public ActionResult ChangePassword()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            Teacher teacher = data.GetTeacher(loginDetails.Username);
            return View(teacher);
        }
        public ActionResult ChangePasswordReq(string OldPassword, string Password)
        {
            if (OldPassword == null || OldPassword.Equals("") || Password == null || Password.Equals(""))
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            Teacher teacher = data.GetTeacher(loginDetails.Username);
            if (!loginDetails.Password.Equals(OldPassword))
            {
                return RedirectToAction("PageNotFoundError", "Home");
            }
            else
            {
                data.ChangePassword(teacher.Id, Password);
                loginDetails.Password = Password;
                Session["loginDetails"] = loginDetails;
            }
            return RedirectToAction("Home");
        }
        public ActionResult AddNewClass()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            Teacher teacher = data.GetTeacher(loginDetails.Username);
            return View(teacher);
        }
        public ActionResult NewClassReq(string Title)
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            
            data.AddCourse(loginDetails.Username, Title);
            return RedirectToAction("Home");
        }
        
        public new ActionResult Profile()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            Teacher teacher = data.GetTeacher(loginDetails.Username);
            return View(teacher);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Home", "Home");
        }
        public ActionResult Class(Parameter p)
        {
            if (p.Id == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            Course course = data.GetCourse(p.Id);
            if (course == null || !course.Teacher.Username.Equals(loginDetails.Username))
                return RedirectToAction("PageNotFoundError", "Home");

            Session["selectedClass"] = p.Id;
            return View(new TeacherCourseViewModel { course = course });
        }
        public ActionResult MarkInactive()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            if (Session["selectedClass"] == null)
                return RedirectToAction("PageNotFoundError", "Home");

            int id = (int)Session["selectedClass"];
            Course course = data.GetCourse(id);
            if (course == null || !course.Teacher.Username.Equals(loginDetails.Username))
                return RedirectToAction("PageNotFoundError", "Home");

            data.SetActive(id, false);
            return RedirectToAction("Class", new { Id = id });
        }

        public ActionResult MarkActive()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            if (Session["selectedClass"] == null)
                return RedirectToAction("PageNotFoundError", "Home");

            int id = (int)Session["selectedClass"];
            Course course = data.GetCourse(id);
            if (course == null || !course.Teacher.Username.Equals(loginDetails.Username))
                return RedirectToAction("PageNotFoundError", "Home");

            data.SetActive(id, true);
            return RedirectToAction("Class", new { Id = id });
        }

        string GeneratePIN()
        {
            string PIN = "";
            Random random = new Random();
            int length = random.Next(10, 15);
            for (int i = 0; i < length; i++)
            {
                int ch = random.Next(1, 3);
                if (ch == 1)
                {
                    // Generate a digit
                    char r = (char)random.Next((int)'0', (int)'9');
                    PIN += r;
                }
                else if (ch == 2)
                {
                    // Generate a lowercase
                    char r = (char)random.Next((int)'a', (int)'z');
                    PIN += r;
                }
                else
                {
                    // Generate a uppercase
                    char r = (char)random.Next((int)'A', (int)'Z');
                    PIN += r;
                }
            }
            return PIN;
        }
        void SendMail(string Email, string Subject, string Body)
        {
            try
            {
                var fromAddress = "bcsf12m041virtualclasses@gmail.com";
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

        void EnrollStudentInCourse(string Email, Course course)
        {
            if (data.IsTeacherEmail(Email))
                return;
            string subject = "You were Enrolled in \"" + course.Title + "\" by your Instructor ";
            string body = "";
            Teacher inst = data.GetInstructor(course.Id);
            subject += inst.FirstName + " " + inst.LastName;
            bool exist = data.DoesEmailExist(Email);
            bool confirmed = data.HasConfirmedEmail(Email);
            if (exist && confirmed)
            {
                data.EnrollStudent(Email, course.Id);

                body = subject;
                body += "\nYou can find this course in your courses at Virtual Classes by Logging In.\n";
                body += "Login here " + link + "Home/Login and see your courses there.\n";
                body += "\nVirtual Classes.\n";
            }
            else
            {
                if (!exist)
                    data.AddStudentByEmail(Email);
                data.EnrollStudent(Email, course.Id);

                string PIN = GeneratePIN();
                data.SaveEmailConfirmationPIN(Email, PIN);

                body = subject;
                body += "\nYou are new to Virtual Classes, just go to " + link + "Student/CompleteRegistration?Email=" + Email + "\n";
                body += "Your Email Confirmation PIN is: " + PIN + "\nWrite this Email Confirmation PIN there and Complete Your Registration.\n";
                body += "\nAnd Complete your Registration there. So you can see your courses there, which you are enrolled in.\n";
                body += "\nVirtual Classes.\n";
            }
            SendMail(Email, subject, body);
        }

        void EnrollStudentsFromFile(string fileName, Course course)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader fin = new StreamReader(fs);
            string email = "";
            while ((email = fin.ReadLine()) != null)
            {
                if (!email.Equals(""))
                {
                    EnrollStudentInCourse(email, course);
                }
            }
            fin.Close();
            fs.Close();
            if (System.IO.File.Exists(fileName))
                System.IO.File.Delete(fileName);
        }

        public ActionResult EnrollStudentsFromFile()
        {
            if (Request.Files.Count != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
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

            HttpPostedFileBase file = Request.Files[0];
            file.SaveAs(Server.MapPath(@"~\Files\" + file.FileName));
            string filePath = Server.MapPath(@"~\Files\" + file.FileName);

            EnrollStudentsFromFile(filePath, course);
            return RedirectToAction("EnrollStudents");
        }

        public ActionResult EnrollStudents(string Email)
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
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
            if (Email == null)
                return View(new TeacherCourseViewModel { course = course });

            EnrollStudentInCourse(Email, course);
            return View(new TeacherCourseViewModel { course = course });
        }
        public ActionResult EnrolledStudents()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            object o = Session["selectedClass"];
            if (o == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int id = (int)o;
            Course course = data.GetCourse(id);
            if (course == null)
                return RedirectToAction("PageNotFoundError", "Home");
            return View(new TeacherCourseViewModel { course = course });
        }
        public ActionResult ScheduleQuiz()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
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

            return View(new TeacherCourseViewModel { course = course });
        }
        public ActionResult ScheduleQuizReq()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
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

            int total;
            if (!int.TryParse(Request["TotalQuestions"], out total))
            {
                ViewBag.IsWrongTotal = true;
                return View("ScheduleQuiz", course);
            }
            if (total <= 0)
            {
                ViewBag.IsWrongTotal = true;
                return View("ScheduleQuiz", course);
            }

            Quiz quiz = new Quiz();
            quiz.Title = Request["Title"];
            quiz.Content = Request["Content"];
            quiz.TotalMarks = total;
            List<QuizQuestion> questions = new List<QuizQuestion>();

            for (int i = 1; i <= total; i++)
            {
                QuizQuestion question = new QuizQuestion();
                question.Question = Request["Question" + i];
                question.OptionA = Request["Q" + i + "OptionA"];
                question.OptionB = Request["Q" + i + "OptionB"];
                question.OptionC = Request["Q" + i + "OptionC"];
                question.OptionD = Request["Q" + i + "OptionD"];
                question.CorrectOption = int.Parse(Request["Correct" + i]);
                question.Marks = 1;
                questions.Add(question);
            }
            data.AddQiuz(course, quiz, questions);
            return RedirectToAction("ScheduleQuiz");
        }
        public ActionResult Quiz(Parameter p)
        {
            if (p.Id == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
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
            return View(new TeacherCourseViewModel { course = course, quiz = q });
        }
        public ActionResult QuizzesResult()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            object o = Session["selectedClass"];
            if (o == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int id = (int)o;
            Course course = data.GetCourse(id);
            if (course == null)
                return RedirectToAction("PageNotFoundError", "Home");

            return View(new TeacherCourseViewModel { course = course });
        }
        public ActionResult GiveAssignment()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
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
            return View(new TeacherCourseViewModel { course = course });
        }
        public ActionResult GiveAssignmentReq()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
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

            int total;
            if (!int.TryParse(Request["TotalQuestions"], out total))
            {
                ViewBag.IsWrongTotal = true;
                return View("ScheduleQuiz", course);
            }
            if (total <= 0)
            {
                ViewBag.IsWrongTotal = true;
                return View("ScheduleQuiz", course);
            }

            Assignment assignment = new Assignment();
            assignment.Title = Request["Title"];
            assignment.Content = Request["Content"];
            List<AssignmentQuestion> questions = new List<AssignmentQuestion>();

            for (int i = 1; i <= total; i++)
            {
                AssignmentQuestion question = new AssignmentQuestion();
                question.Question = Request["Question" + i];
                questions.Add(question);
            }
            data.AddAssignment(course, assignment, questions);
            return RedirectToAction("GiveAssignment");
        }
        public ActionResult Assignment(Parameter p)
        {
            if (p.Id == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
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

            return View(new TeacherCourseViewModel { course = course, assignment = a });
        }
        public ActionResult MakeAnnouncement()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
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
            
            return View(new TeacherCourseViewModel { course = course });
        }
        public ActionResult Announcement(Parameter p)
        {
            if (p.Id == 0)
                return RedirectToAction("PageNotFoundError", "Home");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
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
            return View(new TeacherCourseViewModel { course = course, announcement = a });
        }
        public ActionResult Announce(Announcement announcement)
        {
            if (announcement.Body == null || announcement.Title == null)
                return RedirectToAction("MakeAnnouncement");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
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

            announcement.Time = DateTime.Now.ToString();
            data.MakeAnnouncement(course.Id, announcement);
            return RedirectToAction("MakeAnnouncement");
        }
        public ActionResult StartDiscussion()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
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

            return View(new TeacherCourseViewModel { course = course });
        }
        public ActionResult Discuss(Discussion discussion)
        {
            if (discussion.Title == null || discussion.Body == null)
                return RedirectToAction("StartDiscussion");
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            object o = Session["selectedClass"];
            if (o == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int id = (int)o;
            Course course = data.GetCourse(id);
            if (course == null)
                return RedirectToAction("PageNotFoundError", "Home");

            discussion.Time = DateTime.Now.ToString();
            discussion.FullName = course.Teacher.FirstName + " " + course.Teacher.LastName;
            discussion.Username = course.Teacher.Username;

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
            if (user != 1)
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
            return View(new TeacherCourseViewModel { course = course, discussion = d });
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
            if (user != 1)
                return this.Json(c);
            if (comm == null)
                return this.Json(c);

            int did = (int)Session["discussionSelected"];
            int cid = (int)Session["selectedClass"];
            DiscussionComment comment = new DiscussionComment();
            comment.Body = comm;
            comment.Time = DateTime.Now.ToString();
            Teacher t = data.GetInstructor(cid);
            comment.FullName = t.FirstName + " " + t.LastName;
            comment.Username = t.Username;
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
        public JsonResult Search(string query)
        {
            int classId = (int)Session["selectedClass"];
            SearchResults results = data.Search(query, classId);
            return this.Json(results, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PageNotFoundError()
        {
            LoginDetails loginDetails = (LoginDetails)Session["loginDetails"];
            if (loginDetails == null)
                return RedirectToAction("PageNotFoundError", "Home");
            int user = (int)Session["user"];
            if (user != 1)
                return RedirectToAction("PageNotFoundError", "Home");
            Teacher teacher = data.GetTeacher(loginDetails.Username);
            return View(teacher);
        }
	}
}