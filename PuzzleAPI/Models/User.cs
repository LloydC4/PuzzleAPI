using System.ComponentModel.DataAnnotations;

namespace PuzzleAPI.Models
{
    // model used to create the database table
    public class User
    {
        // primary key
        [Key]
        public int userId
        {
            get;
            set;
        }

        // username
        [Required]
        public string appId
        {
            get;
            set;
        }

        // current score
        [Required]
        public int points
        {
            get;
            set;
        }
    }
}
