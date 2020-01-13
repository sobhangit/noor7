using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace noor7.Dtos.Edit
{

    public class UpdateDtoForNotebook
    {
        public int[] studentIdsForUpdate { get; set; }
        public string[] Grades { get; set; }
        public string examDateForUpdate { get; set; }
    }

}