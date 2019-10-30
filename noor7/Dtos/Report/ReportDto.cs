using noor7.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace noor7.Dtos.Report
{
    public class ReportDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public int PercentOfWork { get; set; }
        public int SeeNumbers { get; set; }
        public int PercentOfClass { get; set; }

    }
}