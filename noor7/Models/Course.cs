using System;
using System.Collections.Generic;

namespace noor7.Models
{
    public class Course
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public string Title { get; set; }
        public  Student Student { get; set; }

        public  ICollection<Exam> Exams { get; set; }
        public  ICollection<Practice> Practices { get; set; }
    }
}