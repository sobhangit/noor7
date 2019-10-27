using noor7.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace noor7.Dtos.Report
{
    public class ExamForReportDto
    {
        public int CourseID { get; set; }
       
        public int Grade { get; set; }
        
        public int FinalGrade { get; set; }
        
        public DateTime ExamDate { get; set; }
        
        public ExamType ExamType { get; set; }
    }
}