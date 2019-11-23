using noor7.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace noor7.Models
{
    public class Exam
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public float Grade { get; set; }
        [Required]
        public int FinalGrade { get; set; }
        [Required]
        public DateTime ExamDate { get; set; }
        [Required]
        public ExamType ExamType { get; set; }
        [Required]
        public float TeacherAdvice { get; set; }
        public Course Course { get; set; }

    }
}