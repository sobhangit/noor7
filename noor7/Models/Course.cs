using System;
using System.Collections.Generic;

namespace noor7.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Practice> Practices { get; set; }
    }
}