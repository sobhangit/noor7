using System;

namespace noor7.Models
{
    public enum ExamType
    {
        کلاسی,هفتگی,ماهیانه,میانی,پایانی,مستمر,ارزشیابی
    }
    public class Exam
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public int Grade { get; set; }
        public int FinalGrade { get; set; }
        public DateTime ExamDate { get; set; }
        public ExamType? ExamType { get; set; }
        public virtual Course Course { get; set; }

    }
}