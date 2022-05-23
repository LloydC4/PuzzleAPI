using PuzzleAPI.Interfaces;
using PuzzleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleAPI.DAL
{
    public class AnswersDAL : IAnswers
    {
        //Creating DBContext object for EF Core
        private DBContext _context;
        public AnswersDAL(DBContext context)
        {
            _context = context;
        }

        // returns all answers to a specific question
        public List<Answers> GetAnswers(int questionId)
        {
            List<Answers> result;
            try
            {
                result = _context.answers.Where(p => p.questionId == questionId).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        // returns single answer
        public Answers GetAnswer(int answerId)
        {
            Answers result;
            try
            {
                result = _context.answers.Where(p => p.answerId == answerId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        // creates answer linked to specific question
        public void AddAnswer(string Answer, int questionId, bool isCorrect)
        {
            Answers newAnswer = new Answers { questionId = questionId, answer = Answer, isCorrect = isCorrect };
            try
            {
                if (_context.answers.Where(p => p.questionId == questionId).Count() < 4)
                {
                    if (_context.answers.Where(p => p.questionId == questionId && p.isCorrect == isCorrect).Count() >= 1 && isCorrect)
                    {

                    }
                    else
                    {
                        _context.answers.Add(newAnswer);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // deletes specified answer
        public void DeleteAnswer(int answerId)
        {
            Answers result;
            try
            {
                result = _context.answers.Where(p => p.answerId == answerId).FirstOrDefault();
                if (result != null && !result.isCorrect && _context.answers.Where(p => p.questionId == result.questionId).Count() > 1)
                {
                    _context.answers.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // updates specified answer
        public void UpdateAnswer(Answers answer)
        {
            Answers result;
            try
            {
                result = _context.answers.Where(p => p.answerId == answer.answerId).FirstOrDefault();
                if (result != null)
                {
                    result.answer = answer.answer;
                    result.isCorrect = answer.isCorrect;
                    result.questionId = answer.questionId;
                   _context.answers.Update(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // checks answers users submitted to see if they are correct and applies correct score
        public int SubmitAnswer(string appId, AnswersSubmitted answersSubmitted, bool passed)
        {
            // checking if the answer selected by the user is correct or not.
            int result = _context.answers.Where(p => p.questionId == answersSubmitted.questionId && p.answerId == answersSubmitted.answerId && p.isCorrect == true).Count();
            if (result > 0 || passed) // answer is correct or passed
            {
                int totalPoints;
                // checking if the hint was used by user.
                if (passed)
                {
                    totalPoints = 0;
                }
                else if (answersSubmitted.hintUsed)
                {
                    totalPoints = 1;
                }
                else
                {
                    totalPoints = 2;
                }

                // updating Question in UserQuestionTable so that next question will be a new question.
                bool isUpdated = UpdateUserQuestion(appId, answersSubmitted.questionId); 
                if (isUpdated)
                {
                    SavePoints(appId, totalPoints); // saving points in users table for this username.
                    return totalPoints;
                }

            }
            return 0;
        }

        // checks answers users submitted to see if they are correct and applies correct score
        public int SubmitTextAnswer(string appId, string textAnswer, AnswersSubmitted answersSubmitted, bool passed)
        {
            // checking if the answer selected by the user is correct or not.
            int result = _context.answers.Where(p => p.questionId == answersSubmitted.questionId && p.answerId == answersSubmitted.answerId && 
                            p.answer.ToLower() == textAnswer.ToLower() && p.isCorrect == true).Count();
            if (result > 0 || passed) // answer is correct or passed.
            {
                int totalPoints;
                if (passed)
                {
                    totalPoints = 0;
                }
                else if (answersSubmitted.hintUsed)
                {
                    totalPoints = 1;
                }
                else
                {
                    totalPoints = 2;
                }

                // updating Question in UserQuestionTable so that next question will be a new question.
                bool isUpdated = UpdateUserQuestion(appId, answersSubmitted.questionId); 
                if (isUpdated)
                {
                    SavePoints(appId, totalPoints); // saving points in users table for this username.
                    return totalPoints;
                }

            }
            return 0;
        }

        // updates user points
        private void SavePoints(string appId, int points)
        {
            // getting relevant user by username and updating points.
            var user = _context.users.Where(p => p.appId == appId).FirstOrDefault();
            if (user != null)
            {
                user.points = user.points + points;
                _context.users.Update(user);
                _context.SaveChanges();
            }
        }

        // moves user to next question if correct answer given for previous question
        private bool UpdateUserQuestion(string appId, int questionId)
        {
            // getting relevant user by username
            var user = _context.users.Where(p => p.appId == appId).FirstOrDefault();
            if (user != null)
            {
                // updating next question
                var userQuestion = _context.userQuestions.Where(p => p.userId == user.userId && p.questionId == questionId && p.isCorrectOrPassed == false).FirstOrDefault();
                if (userQuestion != null)
                {
                    userQuestion.isCorrectOrPassed = true;
                    _context.userQuestions.Update(userQuestion);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
