using Microsoft.EntityFrameworkCore;

namespace PuzzleAPI.Models
{
    public class DBContext : DbContext
    {
        // DBContext class that will handle all database features
        public DBContext(DbContextOptions options) : base(options) { }

        // creating QuestionsCategory object to access QuestionsCategory info from database.
        public DbSet<QuestionCategory> questionCategories
        {
            get;
            set;
        }

        // creating Questions object to access Questions info from database.
        public DbSet<Questions> questions
        {
            get;
            set;
        }

        // creating Answers object to access Answers info from database.
        public DbSet<Answers> answers
        {
            get;
            set;
        }

        // creating Users object to access Users info from database.
        public DbSet<User> users
        {
            get;
            set;
        }
        // creating UserQuestions object to access User's questions from database.
        public DbSet<UserQuestions> userQuestions
        {
            get;
            set;
        }
    }
}
