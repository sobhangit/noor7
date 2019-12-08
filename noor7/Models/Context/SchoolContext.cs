using System.Data.Entity;

namespace noor7.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SchoolContext,Migrations.Configuration>());
        }

        //Set your Table
        public DbSet<Student> Students { get; set; }
        public DbSet<Late> Lates { get; set; }
        public DbSet<Absent> Absents { get; set; }
        public DbSet<Speak> Speaks { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Notebook> Notebooks { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<Job> Jobs { get; set; }
       
    }
}