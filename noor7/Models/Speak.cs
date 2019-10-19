using System;
using System.ComponentModel.DataAnnotations;

namespace noor7.Models
{
    public class Speak
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        [Required]
        public string Problem { get; set; }
        [Required]
        public string Result { get; set; }
        [Required]
        public DateTime SpeakDate { get; set; }
        public Student Student { get; set; }
    }
}