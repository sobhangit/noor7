using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace noor7.Dtos.Report
{
    public class ReportDto
    {

        public string CourseName { get; set; }
        public int PercentOfWork { get; set; }
        public int SeeNumbers { get; set; }
        public int PercentOfClass { get; set; }

    }
}