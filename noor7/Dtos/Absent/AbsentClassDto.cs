using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace noor7.Dtos
{
    public class AbsentClassDto
    {
        public string StudentID { get; set; }
        public string Problem { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public bool IsCertificate { get; set; }
        public bool IsTrue { get; set; }
    }
}
