using System;

namespace noor7.Models
{
    public class Notebook
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int Grade { get; set; }
        public DateTime NoteBookDate { get; set; }
        public virtual Student Student { get; set; }

    }

}