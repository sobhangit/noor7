using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace noor7.Dtos.Edit
{
    public class SendPracticeToViewDtos
    {
        public Dictionary<int, string> Students { get; set; }
        public List<int> CourseIds { get; set; }
        public Dictionary<int, int> Practices { get; set; }
        public int Numbers { get; set; }
    }
}