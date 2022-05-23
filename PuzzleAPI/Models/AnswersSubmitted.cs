namespace PuzzleAPI.Models
{
    // model used for getting data
    public class AnswersSubmitted
    {
        // links answers to question
        public int questionId
        {
            get;
            set;
        }

        public int answerId
        {
            get;
            set;
        }
        // true if hint was requested, false if not
        public bool hintUsed
        {
            get;
            set;
        }
    }
}
