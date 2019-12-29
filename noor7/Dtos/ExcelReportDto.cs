using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace noor7.Dtos
{
    public class ExcelReportDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public Dictionary<int,string> IdAndCourseName { get; set; }
        public List<Exam> Exams { get; set; }
    }
}