using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnterwellTask.Models
{
    public class FakturaViewModel
    {
        public int FakturaID { get; set; }
        public DateTime DatumStvaranja { get; set; }
        public DateTime DatumDospijeca { get; set; }
        public float UkupnaCijenaBezPoreza { get; set; }
        public float UkupnaCijenaSaPorezom { get; set; }
        public virtual string UserID { get; set; }
        public string PrimateljRacuna { get; set; }
        public string Username { get; set; }
        
    }
}