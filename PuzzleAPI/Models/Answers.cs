using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuzzleAPI.Models
{

    // model used to create the database table
    public class Answers
    {
        // primary Key
        [Key]
        public int answerId
        {
            get;
            set;
        }
        // mentioning Foreign key table name  
        [Display(Name = "questions")]
        public virtual int questionId { get; set; }

        // foreign key
        [ForeignKey("questionId")]
        public virtual Questions questions { get; set; }

        // answer text
        [Required]
        public string answer
        {
            get;
            set;
        }

        // true if answer is correct, false if not
        [Required]
        public bool isCorrect
        {
            get;
            set;
        }
    }
}
