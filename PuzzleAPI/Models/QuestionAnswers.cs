using System.Collections.Generic;

namespace PuzzleAPI.Models
{
    // model used to return complete questions
    public class QuestionAnswers
    {
        // ID of the question
        public int questionId
        {
            get;
            set;
        }
        // question text
        public string questionName
        {
            get;
            set;
        }
        // question hint
        public string questionHint
        {
            get;
            set;
        }
        // list of answers
        public List<AnswerDto> answers
        {
            get;
            set;
        }
    }

    public class AnswerDto
    {
        public int answerId
        {
            get;
            set;
        }
        public string answerName
        {
            get;
            set;
        }
    }
}
