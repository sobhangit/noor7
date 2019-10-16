using System;

namespace noor7.Models
{
    public class Late
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public DateTime LateDate { get; set; }
        public int LateTime { get; set; }
        public string Problem { get; set; } 
        public bool IsTrue { get; set; }
        public virtual Student Student { get; set; }
    }
}