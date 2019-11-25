using System;
using System.ComponentModel.DataAnnotations;

namespace noor7.Models
{
    public class Absent
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        [Required]
        public string Problem { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool? IsCertificate { get; set; }
        public bool? IsTrue { get; set; }

        public  Student Student { get; set; }

    }
}