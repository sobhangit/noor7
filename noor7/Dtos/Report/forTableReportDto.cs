using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace noor7.Dtos.Report
{
    public class forTableReportDto
    {
        public List<CourseForReportDto> CourseForReportDtos { get; set; }
        public List<ReportDto> ReportDtos { get; set; }
        public List<ExamForReportDto> Exams { get; set; }
        public List<float> GradeOfNotebook { get; set; }
        public Dictionary<string,int> Totalpolicy { get; set; }

    }
}