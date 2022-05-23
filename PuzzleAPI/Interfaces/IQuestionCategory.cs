using PuzzleAPI.Models;
using System.Collections.Generic;

namespace PuzzleAPI.Interfaces
{
    public interface IQuestionCategory
    {
        // creates new question category
        void AddQuestionCategory(string questionCategoryName);
        // gets all question categories
        List<QuestionCategory> GetQuestionCategories();
        // gets specified question category
        QuestionCategory GetQuestionCategory(int questionCategoryId);
        // deletes specified question category
        void DeleteQuestionCategory(int questionCategoryId);
        // updates specified question category
        void UpdateQuestionCategory(QuestionCategory questionCategory);
    }
}
