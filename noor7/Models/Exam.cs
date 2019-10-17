using noor7.Enums;
using System;

namespace noor7.Models
{
    public class Exam
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public int Grade { get; set; }
        public int FinalGrade { get; set; }
        public DateTime ExamDate { get; set; }
        public ExamType? ExamType { get; set; }
        public Course Course { get; set; }

    }
}