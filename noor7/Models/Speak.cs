using System;

namespace noor7.Models
{
    public class Speak
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public string Problem { get; set; }
        public string Result { get; set; }
        public DateTime SpeakDate { get; set; }
        public Student Student { get; set; }
    }
}