using System;
using System.ComponentModel.DataAnnotations;

namespace noor7.Models
{
    public class Late
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        [Required]
        public DateTime LateDate { get; set; }
        public int? LateTime { get; set; }
        [Required]
        public string Problem { get; set; }
        public bool? IsTrue { get; set; }
        public  Student Student { get; set; }
    }
}