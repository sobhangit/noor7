﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace noor7.Dtos.Edit
{
    public class SendExamToViewDtos
    {
        public Dictionary<int, string> Students { get; set; }
        public List<int> CourseIds { get; set; }
        public Dictionary<int, float> Exams { get; set; }
        public int FinalGrade { get; set; }
    }
}