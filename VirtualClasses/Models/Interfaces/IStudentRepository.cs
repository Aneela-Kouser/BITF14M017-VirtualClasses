using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualClasses.Models.Interfaces
{
    public interface IStudentRepository
    {
        bool DoesStudentEmailExist(string Email);
        bool DoesStudentEmailConfirmed(string Email);
        string GetEmailConfirmationPIN(string Email);
        bool DoesUsernameExist(string username);
        void SaveStudentData(string Email, Student s);
        Student GetStudent(string username);
        Course GetCourse(int id);
        bool DoesStudentEnrolledInCourse(string username, int cid);
        Assignment GetAssignment(int id);
        Announcement GetAnnouncement(int id);
        void SaveDiscussion(int id, Discussion discussion);
        Discussion GetDiscussion(int id);
        void AddComment(int did, DiscussionComment comment);
        Quiz GetQuiz(int id);
        void SaveQuizSolution(int quizId, int studentId, List<QuizQuestionSolution> solutions);
        bool DoesSolutionExist(int quizId, int studentId);
        QuizSolution GetQuizSolution(int qId, int sId);
        void ChangePassword(int sId, string Password);
    }
}
