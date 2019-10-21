using noor7.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace noor7.Models
{
    public class Defect
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        [Required]
        public DefectType Type { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DefaceDate { get; set; }
        public  Student Student { get; set; }
    }
}