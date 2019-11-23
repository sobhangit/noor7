using System;
using System.ComponentModel.DataAnnotations;

namespace noor7.Models
{
    public class Practice
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        [Required]
        public int Numbers { get; set; }
        [Required]
        public int PassedNumbers { get; set; }
        [Required]
        public DateTime PracticeDate { get; set; }
        [Required]
        public int TeacherAdvice { get; set; }
        public  Course Course { get; set; }
    }
}