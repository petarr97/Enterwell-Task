using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnterwellTask.Models
{
    public class RequestModel
    {
        public List<ListRequestModel> Stavke { get; set; }
        public string DatumDospijeca { get; set; }
        public string Primatelj { get; set; }
    }
}