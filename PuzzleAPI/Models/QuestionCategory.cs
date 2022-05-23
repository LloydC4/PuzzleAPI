using System.ComponentModel.DataAnnotations;

namespace PuzzleAPI.Models
{
    // model used to create the database table
    public class QuestionCategory
    {
        // primary key
        [Key]
        public int questionCategoryId
        {
            get;
            set;
        }
        
        // question category name
        [Required]
        [StringLength(250)]
        [MinLength(1)]
        public string categoryName
        {
            get;
            set;
        }
    }
}
