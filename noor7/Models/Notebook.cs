using System;
using System.ComponentModel.DataAnnotations;

namespace noor7.Models
{
    public class Notebook
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        [Required]
        public float Grade { get; set; }
        [Required]
        public DateTime NoteBookDate { get; set; }
        public  Student Student { get; set; }

    }

}