﻿using System;
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
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public int InSchoolFrom { get; set; }
        public string Class { get; set; }
        public int Code { get; set; }
        public int ClassListNumber { get; set; }

        public  ICollection<Course> Courses { get; set; }
        public  ICollection<Notebook> Notebooks { get; set; }
        public  ICollection<Defect> Defects { get; set; }
        public  ICollection<Absent> Absents { get; set; }
        public  ICollection<Late> Lates { get; set; }
        public  ICollection<Speak> Speaks { get; set; } 


    }
}