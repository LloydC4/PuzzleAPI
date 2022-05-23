using PuzzleAPI.Models;
using System.Collections.Generic;

namespace PuzzleAPI.Interfaces
{
    public interface IQuestions
    {
        // creates new question
        void AddQuestion(string questionText, string hint, int questionCategory);
        // gets all questions and answers
        List<QuestionAnswers> GetQuestions();
        // gets specified question and answers
        QuestionAnswers GetQuestion(int questionId);
        // deletes specified question
        void DeleteQuestion(int questionId);
        // updates specified question
        void UpdateQuestion(Questions question);
        // creates quiz for user
        QuestionAnswers GenerateQuiz(int categoryId, int numberOfQuestions, string appId);
        // gets the next question in the quiz, returns same question if previous question not answered correctly or passed
        QuestionAnswers GetUserQuestion(int userId);
    }
}
