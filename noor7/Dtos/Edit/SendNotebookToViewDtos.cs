using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace noor7.Dtos.Edit
{
    public class SendNotebookToViewDtos
    {
        public Dictionary<int, string> Students { get; set; }
        public List<float> Notebooks { get; set; }
    }
}