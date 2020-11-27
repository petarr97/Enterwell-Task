using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnterwellTask.Models
{
    public class CreateFakturaModel
    {
        public Faktura Faktura { get; set; }
        public List<Stavka> Stavke { get; set; }
        
    }
}