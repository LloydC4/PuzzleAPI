using PuzzleAPI.Models;
using System.Collections.Generic;

namespace PuzzleAPI.Interfaces
{
    public interface IAnswers
    {
        // gets all answers
        List<Answers> GetAnswers(int questionId);
        // gets specified answer
        Answers GetAnswer(int answerId);
        // creates new answer
        void AddAnswer(string Answer, int questionId, bool isCorrect);
        // deletes specified answer
        void DeleteAnswer(int answerId);
        // updates specified answer
        void UpdateAnswer(Answers answer);
        // checks if user's submitted answer is correct and assigns points
        int SubmitAnswer(string appId, AnswersSubmitted answersSubmitted, bool passed);
        // checks if user's submitted string is correct and assigns points
        int SubmitTextAnswer(string appId, string textAnswer, AnswersSubmitted answersSubmitted, bool passed);
    }

}
