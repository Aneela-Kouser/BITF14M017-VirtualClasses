using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualClasses.Models;
using VirtualClasses.Models.ViewModels;
using VirtualClasses.Models.Interfaces;
using System.Net;

namespace VirtualClasses.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository data;
        const string link = "http://bcsf12m039virtualclasses.apphb.com/";
        public HomeController(IUserRepository u)
        {
            data = u;
        }
        public ActionResult Home()
        {
            if (Session["loginDetails"] == null || Session["user"] == null)
                return View();
            else {
                LoginDetails det = (LoginDetails)Session["loginDetails"];
                int ret = (int)Session["user"];
                if (ret == 1)
                    return RedirectToAction("Home", "Teacher");
                else
                    return RedirectToAction("Home", "Student");
            }
        }

        public ActionResult Login(LoginDetails log)
        {
            if (Session["loginDetails"] == null || Session["user"] == null)
            {
                if (log.Username == null || log.Password == null) {
                    return View(false);
                } else {
                    // ret is 0 if login details are invalid, 1 if these are teacher credentials, 2 if they're student's
                    int ret = data.CheckLoginDetails(log);
                    if (ret == 0)
                        return View(true);
                    else {
                        Session.Clear();
                        Session["loginDetails"] = log;
                        Session["user"] = ret;
                        data.RemovePasswordResetPIN(log.Username);
                        if (ret == 1)
                            return RedirectToAction("Home", "Teacher");
                        else
                            return RedirectToAction("Home", "Student");
                    }
                }
            } else {
                LoginDetails det = (LoginDetails)Session["loginDetails"];
                int ret = (int)Session["user"];
                if (ret == 1)
                    return RedirectToAction("Home", "Teacher");
                else
                    return RedirectToAction("Home", "Student");
            }
        }
        public JsonResult DoesUsernameExist(string un)
        {
            return this.Json(data.DoesUsernameExist(un), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DoesEmailExist(string em)
        {
            return this.Json(data.DoesEmailExist(em), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SignUp(Teacher teacher)
        {
            if (Session["loginDetails"] == null || Session["user"] == null) {
                if (teacher.Email == null || teacher.Username == null) {
                    SignUpViewModel vm = new SignUpViewModel();
                    vm.data = new Teacher();
                    vm.doesEmailExist = false;
                    vm.doesUsernameExist = false;
                    return View(vm);
                } else {
                    SignUpViewModel vm = new SignUpViewModel();
                    bool f = false;
                    vm.doesUsernameExist = false;
                    vm.doesEmailExist = false;
                    if (data.DoesUsernameExist(teacher.Username)) {
                        vm.doesUsernameExist = true;
                        vm.data = teacher;
                        f = true;
                    }
                    if (data.DoesEmailExist(teacher.Email)) {
                        vm.doesEmailExist = true;
                        vm.data = teacher;
                        f = true;
                    }
                    if (f)
                        return View(vm);
                    Session.Clear();

                    teacher.EmailConfirmed = false;
                    teacher.EmailConfirmationPIN = GeneratePIN();

                    data.AddTeacher(teacher);
                    string subject = "Confirm Your Email";
                    string body = "Thanks for joining Virtual Classes.\n";
                    body += "Please confirm your email by going to this link " + link + "Home/CompleteRegistration to ";
                    body += "Complete Your Registration and Confirm Your Email.\n";
                    body += "Your Email Confirmation PIN is: " + teacher.EmailConfirmationPIN + "\n";
                    body += "Write this Email Confirmation PIN at Complete Registration Form to Confirm your Email.\n";
                    body += "If you didn't sign up for this, you can just ignore this email.\n";
                    body += "\nVirtual Classes\n";
                    SendMail(teacher.Email, subject, body);

                    Session["signUpUsername"] = teacher.Username;
                    return RedirectToAction("Login");
                }
            } else {
                LoginDetails det = (LoginDetails)Session["loginDetails"];
                int ret = (int)Session["user"];
                if (ret == 1)
                    return RedirectToAction("Home", "Teacher");
                else
                    return RedirectToAction("Home", "Student");
            }
        }

        public ActionResult CompleteRegistration(string PIN, string Phone, string Website)
        {
            string username = (string)Session["signUpUsername"];
            if (username == null) {
                return RedirectToAction("PageNotFoundError");
            }
            if (PIN == null)
                return View();

            if (!data.CheckEmailConfirmationPIN(username, PIN))
                return View(new ForgotPasswordViewModel { Message = "Email Confirmation PIN not correct", IsMessageDanger = true });

            data.AddUserDetails(username, Phone, Website, true);
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult ResendEmailConfirmation()
        {
            string username = (string)Session["signUpUsername"];
            if (username == null) {
                return RedirectToAction("PageNotFoundError");
            }
            string Email = data.GetUserMail(username);
            string EmailConfirmationPIN = data.GetUserEmailConfirmationPIN(username);
            string subject = "Confirm Your Email";
            string body = "Thanks for joining Virtual Classes.\n";
            body += "Please confirm your email by going to this link " + link + "Home/CompleteRegistration to ";
            body += "Complete Your Registration and Confirm Your Email.\n";
            body += "Your Email Confirmation PIN is: " + EmailConfirmationPIN + "\n";
            body += "Write this Email Confirmation PIN at Complete Registration Form to Confirm your Email.\n";
            body += "If you didn't sign up for this, you can just ignore this email.\n";
            body += "\nVirtual Classes\n";
            SendMail(Email, subject, body);
            return RedirectToAction("CompleteRegistration");
        }
        string GeneratePIN()
        {
            string PIN = "";
            Random random = new Random();
            int length = random.Next(10, 15);
            for (int i = 0; i < length; i++) {
                int ch = random.Next(1, 3);
                if (ch == 1) {
                    // Generate a digit
                    char r = (char)random.Next((int)'0', (int)'9');
                    PIN += r;
                } else if (ch == 2) {
                    // Generate a lowercase
                    char r = (char)random.Next((int)'a', (int)'z');
                    PIN += r;
                } else {
                    // Generate a uppercase
                    char r = (char)random.Next((int)'A', (int)'Z');
                    PIN += r;
                }
            }
            return PIN;
        }
        void SendMail(string Email, string Subject, string Body)
        {
            try {
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

        public ActionResult ForgotPassword(string Username)
        {
            Session.Clear();
            if (Username == null) {
                return View();
            } else {
                if (data.DoesUsernameExist(Username)) {
                    string PIN = GeneratePIN();
                    data.SavePIN(Username, PIN);

                    string Email = data.GetUserMail(Username);
                    string subject = "Forgot Your Password? Virtual Classes";
                    string body = "Someone reported with your username, that you forgot your password.\n";
                    body += "If this was you, then follow the link " + link + "Home/ResetPassword to ";
                    body += "Reset your Password.\n";
                    body += "And write the pin there.\n";
                    body += "Your PIN: " + PIN + "\n";
                    body += "If this wasn't you, you can just ignore this mail.\n";
                    body += "Virtual Classes\n";
                    SendMail(Email, subject, body);

                    return View(new ForgotPasswordViewModel { Message="We've sent you a mail, please visit your mail to Reset Your Password. If mail isn't in your inbox, check your Spam too.", IsMessageDanger = false });
                } else {
                    return View(new ForgotPasswordViewModel { Message="Username doesn't exist.", IsMessageDanger = true });
                }
            }
        }
        public ActionResult ResetPassword(string Username, string PIN)
        {
            if (Username == null || PIN == null) {
                return View();
            } else {
                if (!data.CheckPIN(Username, PIN)) {
                    return View(new ForgotPasswordViewModel { Message="Username or PIN not correct." });
                }
                Session["passwordResetUser"] = Username;
                return RedirectToAction("NewPassword");
            }
        }
        public ActionResult NewPassword(string Password)
        {
            string username = (string)Session["passwordResetUser"];
            if (username == null) {
                return RedirectToAction("PageNotFoundError");
            }
            if (Password == null) {
                return View();
            } else {
                data.ChangePassword(username, Password);
                data.RemovePasswordResetPIN(username);
                Session.Clear();
                return RedirectToAction("Login");
            }
        }
        public ActionResult PageNotFoundError()
        {
            LoginDetails det = (LoginDetails)Session["loginDetails"];
            if (det == null)
                return View();
            int ret = (int)Session["user"];
            if (ret == 1)
                return RedirectToAction("PageNotFoundError", "Teacher");
            else
                return RedirectToAction("PageNotFoundError", "Student");
        }
	}
}