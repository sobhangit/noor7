using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace noor7.Models
{
    public class Student
    {

        public Int32 Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String FatherName { get; set; }
        public Int32 InSchoolFrom { get; set; }
        public String Class { get; set; }
        public Int32 Code { get; set; }
        public Int32 ClassListNumber { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Notebook> Notebooks { get; set; }
        public virtual ICollection<Defect> Defects { get; set; }
        public virtual ICollection<Absent> Absents { get; set; }
        public virtual ICollection<Late> Lates { get; set; }
        public virtual ICollection<Speak> Speaks { get; set; } 


    }
}