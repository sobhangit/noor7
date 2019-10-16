using System;

namespace noor7.Models
{
    public class Practice
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public int Numbers { get; set; }
        public int PassedNumbers { get; set; }
        public DateTime PracticeDate { get; set; }
        public virtual Course Course { get; set; }
    }
}