using PuzzleAPI.Interfaces;
using PuzzleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleAPI.DAL
{
    public class QuestionsDAL : IQuestions
    {
        //Creating DBContext object for EF Core
        private DBContext _context;
        public QuestionsDAL(DBContext context)
        {
            _context = context;
        }

        // returns all questions with answers
        public List<QuestionAnswers> GetQuestions()
        {
            List<QuestionAnswers> questions = new List<QuestionAnswers>();
            try
            {
                var result = _context.questions.ToList();
                if (result != null)
                {
                    foreach (var question in result)
                    {
                        QuestionAnswers questionAnswers = new QuestionAnswers();
                        questionAnswers.questionId = question.questionId;
                        questionAnswers.questionHint = question.hint;
                        questionAnswers.questionName = question.questionText;
                        // creating answers list
                        List<AnswerDto> answersList = _context.answers.Where(p => p.questionId == questionAnswers.questionId).Select(x => new AnswerDto { answerId = x.answerId, answerName = x.answer }).ToList();
                        questionAnswers.answers = answersList;
                        questions.Add(questionAnswers);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (questions.Count > 0)
            {
                return questions;
            }
            else
            {
                return null;
            }
        }

        // returns specified question with answers
        public QuestionAnswers GetQuestion(int questionId)
        {
            QuestionAnswers questionAnswers = new QuestionAnswers();
            try
            {
                var result = _context.questions.Where(p => p.questionId == questionId).FirstOrDefault();
                if (result != null)
                {
                    questionAnswers.questionId = result.questionId;
                    questionAnswers.questionHint = result.hint;
                    questionAnswers.questionName = result.questionText;
                    // creating answers list
                    List<AnswerDto> answersList = _context.answers.Where(p => p.questionId == questionAnswers.questionId).Select(x => new AnswerDto { answerId = x.answerId, answerName = x.answer }).ToList();
                    questionAnswers.answers = answersList;
                    return questionAnswers;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }

        // creates new question
        public void AddQuestion(string questionText, string hint, int questionCategory)
        {
            try
            {
                Questions newQuestion = new Questions { questionText = questionText, hint = hint, point = 0, questionCategoryId = questionCategory };
                _context.questions.Add(newQuestion);
                _context.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        // deletes specified question and answers
        public void DeleteQuestion(int questionId)
        {
            Questions result;
            try
            {
                // removing question, all answers related to it and all instances of question in active quizzes
                result = _context.questions.Where(p => p.questionId == questionId).FirstOrDefault();
                if (result != null)
                {
                    _context.questions.Remove(result);

                   List<Answers> answersList = _context.answers.Where(p => p.questionId == questionId).ToList();
                    foreach (Answers answer in answersList)
                    {
                        _context.answers.Remove(answer);
                    }

                    List<UserQuestions> userQs = _context.userQuestions.Where(p => p.questionId == questionId).ToList();
                    foreach (UserQuestions question in userQs)
                    {
                        _context.userQuestions.Remove(question);
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // updates specified question
        public void UpdateQuestion(Questions question)
        {
            try
            {
                var result = _context.questions.Where(p => p.questionId == question.questionId).FirstOrDefault();
                if (result != null)
                {
                    result.questionCategoryId = question.questionCategoryId;
                    result.questionText = question.questionText;
                    result.hint = question.hint;
                    result.point = question.point;
                    _context.questions.Update(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }

        // randomly selects specified number of questions from specified question category and assigns list of questions to user
        public QuestionAnswers GenerateQuiz(int categoryId, int numberOfQuestions, string appId)
        {
            List<QuestionAnswers> result = new List<QuestionAnswers>();
            try
            {
                if (numberOfQuestions <= _context.questions.Where(p => p.questionCategoryId == categoryId).Count())
                {
                    // getting list of questions for specific category.
                    List<Questions> questions = _context.questions.Where(p => p.questionCategoryId == categoryId).ToList();
                    foreach (var item in questions)
                    {
                        QuestionAnswers questionAnswers = new QuestionAnswers();
                        questionAnswers.questionId = item.questionId;
                        questionAnswers.questionName = item.questionText;
                        questionAnswers.questionHint = item.hint;
                        // getting list of answers related to the questions
                        List<AnswerDto> answersList = _context.answers.Where(p => p.questionId == item.questionId).Select(x => new AnswerDto { answerId = x.answerId, answerName = x.answer }).ToList();
                        questionAnswers.answers = answersList;
                        // adding questions & answers in a list.
                        result.Add(questionAnswers);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            if (result.Count > 0)
            {
                var rnd = new Random();
                // shuffling the list so that we can get random questions.
                var randomized = result.OrderBy(item => rnd.Next());
                // getting specific number of Questions from List as requested.
                result = randomized.Take(numberOfQuestions).ToList();
            }

            // checking if user already has active quiz
            var user = _context.users.Where(p => p.appId == appId).FirstOrDefault();
            if (user != null)
            {
                int userQuestionCount = _context.userQuestions.Where(p => p.userId == user.userId).Count();
                if (userQuestionCount <= 0)
                {
                    // inserting all selected questions for this user. 
                    List<UserQuestions>? UserQuestionList = new List<UserQuestions>();

                    foreach (QuestionAnswers item in result)
                    {
                        UserQuestions userQuestion = new UserQuestions();
                        userQuestion.isCorrectOrPassed = false;
                        userQuestion.questionId = item.questionId;
                        userQuestion.userId = user.userId;
                        UserQuestionList.Add(userQuestion);
                    }

                    _context.userQuestions.AddRange(UserQuestionList);
                    _context.SaveChanges();
                }
                //return true;
                return GetUserQuestion(user.userId);
            }

            return null;

        }


        // gets the next question for the user to answer
        public QuestionAnswers GetUserQuestion(int userId)
        {
            // fetching next question for relevant user.
            QuestionAnswers questionAnswers = new QuestionAnswers();
            try
            {
                // getting list of questions for specific category.
                var result = _context.userQuestions.Where(p => p.userId == userId && p.isCorrectOrPassed == false).FirstOrDefault();
                if (result != null)
                {
                    Questions question = _context.questions.Where(p => p.questionId == result.questionId).FirstOrDefault();
                    questionAnswers.questionId = question.questionId;
                    questionAnswers.questionHint = question.hint;
                    questionAnswers.questionName = question.questionText;
                    List<AnswerDto> answersList = _context.answers.Where(p => p.questionId == questionAnswers.questionId).Select(x => new AnswerDto { answerId = x.answerId, answerName = x.answer }).ToList();
                    questionAnswers.answers = answersList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

            }
            return questionAnswers;
        }
    }
}
