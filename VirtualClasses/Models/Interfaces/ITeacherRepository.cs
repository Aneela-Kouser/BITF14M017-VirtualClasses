using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualClasses.Models.ViewModels;

namespace VirtualClasses.Models.Interfaces
{
    public interface ITeacherRepository
    {
        Teacher GetTeacher(string username);

        void AddCourse(string username, string title);
        Course GetCourse(int cid);

        bool DoesUsernameExist(string username);
        bool DoesEmailExist(string email);
        void EnrollStudent(string Email, int cid);
        void AddStudentByEmail(string Email);
        Teacher GetInstructor(int cid);
        bool HasConfirmedEmail(string Email);
        void SaveEmailConfirmationPIN(string Email, string PIN);
        bool IsTeacherEmail(string Email);
        void SetActive(int id, bool active);
        void AddQiuz(Course course, Quiz quiz, List<QuizQuestion> questions);
        Quiz GetQuiz(int id);
        void AddAssignment(Course course, Assignment assignment, List<AssignmentQuestion> questions);
        Assignment GetAssignment(int id);
        void MakeAnnouncement(int cid, Announcement announcement);
        Announcement GetAnnouncement(int id);
        void SaveDiscussion(int id, Discussion discussion);
        Discussion GetDiscussion(int id);
        void AddComment(int did, DiscussionComment comment);
        void ChangePassword(int tId, string Password);
        SearchResults Search(string searchQuery, int classId);
    }
}
