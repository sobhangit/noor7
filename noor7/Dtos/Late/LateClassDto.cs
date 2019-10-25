using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace noor7.Dtos.Late
{

    public class LateClassDto
    {
        public string studentID { get; set; }
        public string lateDate { get; set; }
        public string howMuch { get; set; }
        public string problem { get; set; }
        public bool isTrue { get; set; }
    }

}