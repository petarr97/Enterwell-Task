using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnterwellTask.Models
{
    public class Faktura
    {
        [Key]
        public int FakturaID { get; set; }
        public DateTime DatumStvaranja { get; set; }
        public DateTime DatumDospijeca { get; set; }
        public float UkupnaCijenaBezPoreza { get; set; }
        public float UkupnaCijenaSaPorezom { get; set; }
        public virtual string UserID { get; set; }
        public string PrimateljRacuna { get; set; }
        public virtual ICollection<StavkeFakture> StavkeFakture { get; set; }

        public Faktura()
        {
            StavkeFakture = new HashSet<StavkeFakture>();
        }
    }
}