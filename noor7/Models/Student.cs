using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace noor7.Models
{
    public class Student
    {

        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FatherName { get; set; }
        [Required]
        public int InSchoolFrom { get; set; }
        [Required]
        public string Class { get; set; }
        public long? Code { get; set; }
        [Required]
        public int? ClassListNumber { get; set; }
        public bool? Exit { get; set; }

        public  ICollection<Course> Courses { get; set; }
        public  ICollection<Notebook> Notebooks { get; set; }
        public  ICollection<Defect> Defects { get; set; }
        public  ICollection<Absent> Absents { get; set; }
        public  ICollection<Late> Lates { get; set; }
        public  ICollection<Speak> Speaks { get; set; }
        public  ICollection<Job> Jobs { get; set; }


    }
}