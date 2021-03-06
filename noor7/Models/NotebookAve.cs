﻿using noor7.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace noor7.Models
{
    public class NotebookAve
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        [Required]
        public DateTime Month { get; set; }
        [Required]
        public float Average { get; set; }
      
        public Student Student { get; set; }
    }
}