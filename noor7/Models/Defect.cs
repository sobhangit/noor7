using System;

namespace noor7.Models
{
    public enum Type
    {
        انضباطی,علمی
    }
    public class Defect
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public Type Type { get; set; }
        public string Description { get; set; }
        public DateTime DefaceDate { get; set; }
        public virtual Student Student { get; set; }
    }
}