using System.ComponentModel.DataAnnotations;

namespace PuzzleAPI.Models
{
    // model used to create the database table
    public class UserQuestions
    {
        // primary Key
        [Key]
        public int userQuestionsId
        {
            get;
            set;
        }
        // links question to user when quiz is generated
        public int userId
        {
            get;
            set;
        }

        // links object to question
        public int questionId
        {
            get;
            set;
        }
        // used to determine next unanswered question
        public bool isCorrectOrPassed
        {
            get;
            set;
        }
    }
}
