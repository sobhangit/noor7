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
       
        public float Grade { get; set; }
        
        public int FinalGrade { get; set; }
        
        public String ExamDate { get; set; }
        
        public String ExamType { get; set; }
    }
}