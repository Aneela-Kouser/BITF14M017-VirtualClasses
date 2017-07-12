using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualClasses.Models.Interfaces;

namespace VirtualClasses.Models
{
    public class UserRepository : IUserRepository
    {

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

        public void AddTeacher(Teacher teacher)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                data.Teachers.Add(teacher);
                data.SaveChanges();
            }
        }

        public int CheckLoginDetails(LoginDetails log)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                Teacher t = null;
                try
                {
                    t = data.Teachers.First(x => x.Username.Equals(log.Username));
                }
                catch (InvalidOperationException) { }
                
                if (t != null)
                {
                    if (t.Password.Equals(log.Password))
                        return 1;
                    else
                        return 0;
                }
                else
                {
                    Student s = null;
                    try
                    {
                        s = data.Students.First(x => x.Username.Equals(log.Username));
                    }
                    catch (InvalidOperationException) { }
                    
                    if (s != null){
                        if (s.Password.Equals(log.Password))
                            return 2;
                        else
                            return 0;
                    }
                    else
                        return 0;
                }
            }
        }

        public string GetUserMail(string username)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Username.Equals(username)
                             select x).Count();
                if (count != 0)
                    return data.Students.First(x => x.Username.Equals(username)).Email;
                else
                {
                    count = (from x in data.Teachers
                             where x.Username.Equals(username)
                             select x).Count();
                    if (count != 0)
                        return data.Teachers.First(x => x.Username.Equals(username)).Email;
                    else
                        return null;
                }
            }
        }

        public void SavePIN(string username, string PIN)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Username.Equals(username)
                             select x).Count();
                if (count != 0){
                    data.Students.First(x => x.Username.Equals(username)).PasswordResetPIN = PIN;
                    data.SaveChanges();
                }
                else
                {
                    count = (from x in data.Teachers
                             where x.Username.Equals(username)
                             select x).Count();
                    if (count != 0)
                    {
                        data.Teachers.First(x => x.Username.Equals(username)).PasswordResetPIN = PIN;
                        data.SaveChanges();
                    }
                }
            }
        }

        public void RemovePasswordResetPIN(string username)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Username.Equals(username)
                             select x).Count();
                if (count != 0)
                {
                    data.Students.First(x => x.Username.Equals(username)).PasswordResetPIN = "";
                    data.SaveChanges();
                    return;
                }
                count = (from x in data.Teachers
                         where x.Username.Equals(username)
                         select x).Count();
                if (count != 0)
                {
                    data.Teachers.First(x => x.Username.Equals(username)).PasswordResetPIN = "";
                    data.SaveChanges();
                }
            }
        }

        public bool CheckPIN(string Username, string PIN)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Username.Equals(Username) && x.PasswordResetPIN != null && x.PasswordResetPIN.Equals(PIN)
                             select x).Count();
                if (count != 0)
                    return true;
                count = (from x in data.Teachers
                         where x.Username.Equals(Username) && x.PasswordResetPIN != null && x.PasswordResetPIN.Equals(PIN)
                         select x).Count();
                return count != 0;
            }
        }

        public void ChangePassword(string username, string Password)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Username.Equals(username)
                             select x).Count();
                if (count != 0)
                {
                    data.Students.First(x => x.Username.Equals(username)).Password = Password;
                    data.SaveChanges();
                    return;
                }
                count = (from x in data.Teachers
                         where x.Username.Equals(username)
                         select x).Count();
                if (count != 0)
                {
                    data.Teachers.First(x => x.Username.Equals(username)).Password = Password;
                    data.SaveChanges();
                }
            }
        }

        public bool CheckEmailConfirmationPIN(string username, string PIN)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Username.Equals(username) && x.EmailConfirmationPIN != null && x.EmailConfirmationPIN.Equals(PIN)
                             select x).Count();
                if (count != 0)
                {
                    return true;
                }
                count = (from x in data.Teachers
                         where x.Username.Equals(username) && x.EmailConfirmationPIN != null && x.EmailConfirmationPIN.Equals(PIN)
                         select x).Count();
                return count != 0;
            }
        }

        public void AddUserDetails(string username, string Phone, string Website, bool EmailConfirmed)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Username.Equals(username)
                             select x).Count();
                if (count != 0)
                {
                    Student s = data.Students.First(x => x.Username.Equals(username));
                    s.Mobile = Phone;
                    s.Website = Website;
                    s.EmailConfirmed = EmailConfirmed;
                    data.SaveChanges();
                    return;
                }
                count = (from x in data.Teachers
                         where x.Username.Equals(username)
                         select x).Count();
                if (count != 0)
                {
                    Teacher t = data.Teachers.First(x => x.Username.Equals(username));
                    t.Mobile = Phone;
                    t.Website = Website;
                    t.EmailConfirmed = EmailConfirmed;
                    data.SaveChanges();
                }
            }
        }

        public bool DoesUserHasConfirmedEmail(string username)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Username.Equals(username)
                             select x).Count();
                if (count != 0)
                {
                    return data.Students.First(x => x.Username.Equals(username)).EmailConfirmed;
                }
                count = (from x in data.Teachers
                         where x.Username.Equals(username)
                         select x).Count();
                if (count != 0)
                {
                    return data.Teachers.First(x => x.Username.Equals(username)).EmailConfirmed;
                }
                return false;
            }
        }
        public string GetUserEmailConfirmationPIN(string username)
        {
            using (VCDataModelContainer data = new VCDataModelContainer())
            {
                int count = (from x in data.Students
                             where x.Username.Equals(username)
                             select x).Count();
                if (count != 0)
                    return data.Students.First(x => x.Username.Equals(username)).EmailConfirmationPIN;
                else
                {
                    count = (from x in data.Teachers
                             where x.Username.Equals(username)
                             select x).Count();
                    if (count != 0)
                        return data.Teachers.First(x => x.Username.Equals(username)).EmailConfirmationPIN;
                    else
                        return null;
                }
            }
        }
        
    }
}