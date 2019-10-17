using noor7.Enums;
using System;

namespace noor7.Models
{

    public class Defect
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public DefectType Type { get; set; }
        public string Description { get; set; }
        public DateTime DefaceDate { get; set; }
        public  Student Student { get; set; }
    }
}