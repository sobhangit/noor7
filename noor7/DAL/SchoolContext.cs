using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using noor7.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace noor7.DAL
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("SchoolContext"){

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Late> Lates { get; set; }
        public DbSet<Absent> Absents { get; set; }
        public DbSet<Speak> Speaks { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Notebook> Notebooks { get; set; }
        public DbSet<Practice> Practices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}