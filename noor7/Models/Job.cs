using noor7.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace noor7.Models
{
    public class Job
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        [Required]
        public int Cycle { get; set; }
        [Required]
        public JobType JobType { get; set; }
        [Required]
        public int Grade { get; set; }
        public Student Student { get; set; }
    }
}