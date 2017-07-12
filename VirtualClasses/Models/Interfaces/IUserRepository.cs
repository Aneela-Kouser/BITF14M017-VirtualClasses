using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualClasses.Models.Interfaces
{
    public interface IUserRepository
    {
        bool DoesUsernameExist(string username);
        bool DoesEmailExist(string email);
        void AddTeacher(Teacher teacher);
        int CheckLoginDetails(LoginDetails log);

        string GetUserMail(string Username);

        void SavePIN(string Username, string PIN);

        void RemovePasswordResetPIN(string p);

        bool CheckPIN(string Username, string PIN);

        void ChangePassword(string username, string Password);

        bool CheckEmailConfirmationPIN(string username, string PIN);

        void AddUserDetails(string username, string Phone, string Website, bool EmailConfirmed);

        bool DoesUserHasConfirmedEmail(string p);

        string GetUserEmailConfirmationPIN(string username);
    }
}
