
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/20/2016 18:54:26
-- Generated from EDMX file: C:\Users\Ahmad\documents\visual studio 2013\Projects\VirtualClasses\VirtualClasses\Models\VCDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [C:\USERS\AHMAD\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\VIRTUALCLASSES\VIRTUALCLASSES\APP_DATA\VCDATABASE.MDF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CourseTeacher]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Courses] DROP CONSTRAINT [FK_CourseTeacher];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseStudent_Course]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseStudent] DROP CONSTRAINT [FK_CourseStudent_Course];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseStudent_Student]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseStudent] DROP CONSTRAINT [FK_CourseStudent_Student];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseQuiz]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Quizs] DROP CONSTRAINT [FK_CourseQuiz];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizQuizQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizQuestions] DROP CONSTRAINT [FK_QuizQuizQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizQuizSolution]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizSolutions] DROP CONSTRAINT [FK_QuizQuizSolution];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizSolutionStudent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizSolutions] DROP CONSTRAINT [FK_QuizSolutionStudent];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizSolutionQuizQuestionSolution]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizQuestionSolutions] DROP CONSTRAINT [FK_QuizSolutionQuizQuestionSolution];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizQuestionQuizQuestionSolution]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizQuestionSolutions] DROP CONSTRAINT [FK_QuizQuestionQuizQuestionSolution];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseAssignment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Assignments] DROP CONSTRAINT [FK_CourseAssignment];
GO
IF OBJECT_ID(N'[dbo].[FK_AssignmentAssignmentQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssignmentQuestions] DROP CONSTRAINT [FK_AssignmentAssignmentQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseAnnouncement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Announcements] DROP CONSTRAINT [FK_CourseAnnouncement];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseDiscussion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Discussions] DROP CONSTRAINT [FK_CourseDiscussion];
GO
IF OBJECT_ID(N'[dbo].[FK_DiscussionDiscussionComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DiscussionComments] DROP CONSTRAINT [FK_DiscussionDiscussionComment];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Students]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Students];
GO
IF OBJECT_ID(N'[dbo].[Teachers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teachers];
GO
IF OBJECT_ID(N'[dbo].[Courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Courses];
GO
IF OBJECT_ID(N'[dbo].[Quizs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Quizs];
GO
IF OBJECT_ID(N'[dbo].[QuizQuestions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizQuestions];
GO
IF OBJECT_ID(N'[dbo].[QuizSolutions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizSolutions];
GO
IF OBJECT_ID(N'[dbo].[QuizQuestionSolutions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizQuestionSolutions];
GO
IF OBJECT_ID(N'[dbo].[Assignments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Assignments];
GO
IF OBJECT_ID(N'[dbo].[AssignmentQuestions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssignmentQuestions];
GO
IF OBJECT_ID(N'[dbo].[Announcements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Announcements];
GO
IF OBJECT_ID(N'[dbo].[Discussions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Discussions];
GO
IF OBJECT_ID(N'[dbo].[DiscussionComments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DiscussionComments];
GO
IF OBJECT_ID(N'[dbo].[CourseStudent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CourseStudent];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [Username] nvarchar(max)  NULL,
    [Password] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Gender] nvarchar(max)  NULL,
    [Mobile] nvarchar(max)  NULL,
    [Website] nvarchar(max)  NULL,
    [PasswordResetPIN] nvarchar(max)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [EmailConfirmationPIN] nvarchar(max)  NULL
);
GO

-- Creating table 'Teachers'
CREATE TABLE [dbo].[Teachers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [Mobile] nvarchar(max)  NULL,
    [Website] nvarchar(max)  NULL,
    [PasswordResetPIN] nvarchar(max)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [EmailConfirmationPIN] nvarchar(max)  NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [TeacherId] int  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'Quizs'
CREATE TABLE [dbo].[Quizs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CourseId] int  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NULL,
    [TotalMarks] int  NOT NULL
);
GO

-- Creating table 'QuizQuestions'
CREATE TABLE [dbo].[QuizQuestions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Question] nvarchar(max)  NOT NULL,
    [OptionA] nvarchar(max)  NOT NULL,
    [OptionB] nvarchar(max)  NOT NULL,
    [OptionC] nvarchar(max)  NOT NULL,
    [OptionD] nvarchar(max)  NOT NULL,
    [CorrectOption] int  NOT NULL,
    [Marks] int  NOT NULL,
    [QuizId] int  NOT NULL
);
GO

-- Creating table 'QuizSolutions'
CREATE TABLE [dbo].[QuizSolutions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuizId] int  NOT NULL,
    [StudentId] int  NOT NULL,
    [Correct] int  NOT NULL,
    [Marks] int  NOT NULL,
    [Attempted] int  NOT NULL
);
GO

-- Creating table 'QuizQuestionSolutions'
CREATE TABLE [dbo].[QuizQuestionSolutions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SelectedOption] int  NULL,
    [QuizSolutionId] int  NOT NULL,
    [IsCorrect] bit  NOT NULL,
    [QuizQuestionId] int  NOT NULL
);
GO

-- Creating table 'Assignments'
CREATE TABLE [dbo].[Assignments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CourseId] int  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AssignmentQuestions'
CREATE TABLE [dbo].[AssignmentQuestions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Question] nvarchar(max)  NOT NULL,
    [AssignmentId] int  NOT NULL
);
GO

-- Creating table 'Announcements'
CREATE TABLE [dbo].[Announcements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [CourseId] int  NOT NULL,
    [Time] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Discussions'
CREATE TABLE [dbo].[Discussions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [CourseId] int  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [Time] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DiscussionComments'
CREATE TABLE [dbo].[DiscussionComments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [DiscussionId] int  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [Time] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CourseStudent'
CREATE TABLE [dbo].[CourseStudent] (
    [Courses_Id] int  NOT NULL,
    [Students_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Teachers'
ALTER TABLE [dbo].[Teachers]
ADD CONSTRAINT [PK_Teachers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Quizs'
ALTER TABLE [dbo].[Quizs]
ADD CONSTRAINT [PK_Quizs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuizQuestions'
ALTER TABLE [dbo].[QuizQuestions]
ADD CONSTRAINT [PK_QuizQuestions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuizSolutions'
ALTER TABLE [dbo].[QuizSolutions]
ADD CONSTRAINT [PK_QuizSolutions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuizQuestionSolutions'
ALTER TABLE [dbo].[QuizQuestionSolutions]
ADD CONSTRAINT [PK_QuizQuestionSolutions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Assignments'
ALTER TABLE [dbo].[Assignments]
ADD CONSTRAINT [PK_Assignments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AssignmentQuestions'
ALTER TABLE [dbo].[AssignmentQuestions]
ADD CONSTRAINT [PK_AssignmentQuestions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Announcements'
ALTER TABLE [dbo].[Announcements]
ADD CONSTRAINT [PK_Announcements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Discussions'
ALTER TABLE [dbo].[Discussions]
ADD CONSTRAINT [PK_Discussions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DiscussionComments'
ALTER TABLE [dbo].[DiscussionComments]
ADD CONSTRAINT [PK_DiscussionComments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Courses_Id], [Students_Id] in table 'CourseStudent'
ALTER TABLE [dbo].[CourseStudent]
ADD CONSTRAINT [PK_CourseStudent]
    PRIMARY KEY CLUSTERED ([Courses_Id], [Students_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TeacherId] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [FK_CourseTeacher]
    FOREIGN KEY ([TeacherId])
    REFERENCES [dbo].[Teachers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseTeacher'
CREATE INDEX [IX_FK_CourseTeacher]
ON [dbo].[Courses]
    ([TeacherId]);
GO

-- Creating foreign key on [Courses_Id] in table 'CourseStudent'
ALTER TABLE [dbo].[CourseStudent]
ADD CONSTRAINT [FK_CourseStudent_Course]
    FOREIGN KEY ([Courses_Id])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Students_Id] in table 'CourseStudent'
ALTER TABLE [dbo].[CourseStudent]
ADD CONSTRAINT [FK_CourseStudent_Student]
    FOREIGN KEY ([Students_Id])
    REFERENCES [dbo].[Students]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseStudent_Student'
CREATE INDEX [IX_FK_CourseStudent_Student]
ON [dbo].[CourseStudent]
    ([Students_Id]);
GO

-- Creating foreign key on [CourseId] in table 'Quizs'
ALTER TABLE [dbo].[Quizs]
ADD CONSTRAINT [FK_CourseQuiz]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseQuiz'
CREATE INDEX [IX_FK_CourseQuiz]
ON [dbo].[Quizs]
    ([CourseId]);
GO

-- Creating foreign key on [QuizId] in table 'QuizQuestions'
ALTER TABLE [dbo].[QuizQuestions]
ADD CONSTRAINT [FK_QuizQuizQuestion]
    FOREIGN KEY ([QuizId])
    REFERENCES [dbo].[Quizs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuizQuizQuestion'
CREATE INDEX [IX_FK_QuizQuizQuestion]
ON [dbo].[QuizQuestions]
    ([QuizId]);
GO

-- Creating foreign key on [QuizId] in table 'QuizSolutions'
ALTER TABLE [dbo].[QuizSolutions]
ADD CONSTRAINT [FK_QuizQuizSolution]
    FOREIGN KEY ([QuizId])
    REFERENCES [dbo].[Quizs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuizQuizSolution'
CREATE INDEX [IX_FK_QuizQuizSolution]
ON [dbo].[QuizSolutions]
    ([QuizId]);
GO

-- Creating foreign key on [StudentId] in table 'QuizSolutions'
ALTER TABLE [dbo].[QuizSolutions]
ADD CONSTRAINT [FK_QuizSolutionStudent]
    FOREIGN KEY ([StudentId])
    REFERENCES [dbo].[Students]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuizSolutionStudent'
CREATE INDEX [IX_FK_QuizSolutionStudent]
ON [dbo].[QuizSolutions]
    ([StudentId]);
GO

-- Creating foreign key on [QuizSolutionId] in table 'QuizQuestionSolutions'
ALTER TABLE [dbo].[QuizQuestionSolutions]
ADD CONSTRAINT [FK_QuizSolutionQuizQuestionSolution]
    FOREIGN KEY ([QuizSolutionId])
    REFERENCES [dbo].[QuizSolutions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuizSolutionQuizQuestionSolution'
CREATE INDEX [IX_FK_QuizSolutionQuizQuestionSolution]
ON [dbo].[QuizQuestionSolutions]
    ([QuizSolutionId]);
GO

-- Creating foreign key on [QuizQuestionId] in table 'QuizQuestionSolutions'
ALTER TABLE [dbo].[QuizQuestionSolutions]
ADD CONSTRAINT [FK_QuizQuestionQuizQuestionSolution]
    FOREIGN KEY ([QuizQuestionId])
    REFERENCES [dbo].[QuizQuestions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuizQuestionQuizQuestionSolution'
CREATE INDEX [IX_FK_QuizQuestionQuizQuestionSolution]
ON [dbo].[QuizQuestionSolutions]
    ([QuizQuestionId]);
GO

-- Creating foreign key on [CourseId] in table 'Assignments'
ALTER TABLE [dbo].[Assignments]
ADD CONSTRAINT [FK_CourseAssignment]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseAssignment'
CREATE INDEX [IX_FK_CourseAssignment]
ON [dbo].[Assignments]
    ([CourseId]);
GO

-- Creating foreign key on [AssignmentId] in table 'AssignmentQuestions'
ALTER TABLE [dbo].[AssignmentQuestions]
ADD CONSTRAINT [FK_AssignmentAssignmentQuestion]
    FOREIGN KEY ([AssignmentId])
    REFERENCES [dbo].[Assignments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AssignmentAssignmentQuestion'
CREATE INDEX [IX_FK_AssignmentAssignmentQuestion]
ON [dbo].[AssignmentQuestions]
    ([AssignmentId]);
GO

-- Creating foreign key on [CourseId] in table 'Announcements'
ALTER TABLE [dbo].[Announcements]
ADD CONSTRAINT [FK_CourseAnnouncement]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseAnnouncement'
CREATE INDEX [IX_FK_CourseAnnouncement]
ON [dbo].[Announcements]
    ([CourseId]);
GO

-- Creating foreign key on [CourseId] in table 'Discussions'
ALTER TABLE [dbo].[Discussions]
ADD CONSTRAINT [FK_CourseDiscussion]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseDiscussion'
CREATE INDEX [IX_FK_CourseDiscussion]
ON [dbo].[Discussions]
    ([CourseId]);
GO

-- Creating foreign key on [DiscussionId] in table 'DiscussionComments'
ALTER TABLE [dbo].[DiscussionComments]
ADD CONSTRAINT [FK_DiscussionDiscussionComment]
    FOREIGN KEY ([DiscussionId])
    REFERENCES [dbo].[Discussions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DiscussionDiscussionComment'
CREATE INDEX [IX_FK_DiscussionDiscussionComment]
ON [dbo].[DiscussionComments]
    ([DiscussionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------