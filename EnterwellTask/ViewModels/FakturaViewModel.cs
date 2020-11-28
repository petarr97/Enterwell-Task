using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnterwellTask.Models
{
    public class FakturaViewModel
    {
        public int FakturaID { get; set; }

        [DisplayName("Datum stvaranja")]
        public DateTime DatumStvaranja { get; set; }
       
        [DisplayName("Datum dospijeca")]
        public DateTime DatumDospijeca { get; set; }
     
        [DisplayName("Ukupna cijena bez poreza")]
        public float UkupnaCijenaBezPoreza { get; set; }
     
        [DisplayName("Ukupna cijena sa porezom")]
        public float UkupnaCijenaSaPorezom { get; set; }
        public virtual string UserID { get; set; }
      
        [DisplayName("Primatelj racuna")]
        public string PrimateljRacuna { get; set; }
      
        [DisplayName("Korisnik")]
        public string Username { get; set; }
        public List<StavkaViewModel> Stavke { get; set; }
        
    }
}