using PuzzleAPI.Interfaces;
using PuzzleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleAPI.DAL
{
    public class QuestionCategoryDAL: IQuestionCategory
    {
        //Creating DBContext object for EF Core
        private DBContext _context;
        public QuestionCategoryDAL(DBContext context)
        {
            _context = context;
        }

        // returns all question categories
        public List<QuestionCategory> GetQuestionCategories()
        {
            List<QuestionCategory> result;
            try
            {
                result = _context.questionCategories.ToList();
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

        // returns specified question category
        public QuestionCategory GetQuestionCategory(int questionCategoryId)
        {
            QuestionCategory result;
            try
            {
                result = _context.questionCategories.Where(p => p.questionCategoryId == questionCategoryId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        // creates new question category
        public void AddQuestionCategory(string questionCategoryName)
        {
            QuestionCategory test = new QuestionCategory {categoryName = questionCategoryName };
            try
            {
                _context.questionCategories.Add(test);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // deletes specified question category
        public void DeleteQuestionCategory(int questionCategoryId)
        {
            QuestionCategory result;
            try
            {
                result = _context.questionCategories.Where(p => p.questionCategoryId == questionCategoryId).FirstOrDefault();
                List <Questions> questions = _context.questions.Where(p => p.questionCategoryId == questionCategoryId).ToList();

                // deleting question category and all questions in category
                if (result != null)
                {
                    _context.questionCategories.Remove(result);
                    List<UserQuestions> userQuestions = new List<UserQuestions>();
                    if (questions.Count > 0)
                    {
                        foreach (var question in questions)
                        {
                            userQuestions = _context.userQuestions.Where(p => p.questionId == question.questionId).ToList();
                            _context.questions.Remove(question);
                        }
                    }

                    if (userQuestions.Count > 0)
                    {
                        foreach (var userQuestion in userQuestions)
                        {
                            _context.userQuestions.Remove(userQuestion);
                        }
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // updates specified question category
        public void UpdateQuestionCategory(QuestionCategory questionCategory)
        {
            QuestionCategory result;
            try
            {
                result = _context.questionCategories.Where(p => p.questionCategoryId == questionCategory.questionCategoryId).FirstOrDefault();
                if (result != null && questionCategory.categoryName != null)
                {
                    result.categoryName = questionCategory.categoryName;
                    _context.questionCategories.Update(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
