using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuzzleAPI.Models
{
    // model used to create the database table
    public class Questions
    {
        // primary Key
        [Key]
        public int questionId
        {
            get;
            set;
        }
        // mentioning Foreign key table name  
        [Display(Name = "questionCategories")]
        public virtual int questionCategoryId { get; set; }
        // creating foreign key
        [ForeignKey("questionCategoryId")]
        public virtual QuestionCategory questionCategory { get; set; }

        // question text
        [Required]
        public string questionText
        {
            get;
            set;
        }

        // hint to help answer question
        [Required]
        public string hint
        {
            get;
            set;
        }

        // how many points the user will get, 2 if correct, 1 if correct but hint used, 0 if wrong
        public int point
        {
            get;
            set;
        }
    }
}
