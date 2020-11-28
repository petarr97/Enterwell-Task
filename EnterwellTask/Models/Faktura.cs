using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnterwellTask.Models
{
    public class Faktura
    {
        [Key]
        public int FakturaID { get; set; }
        [DisplayName("Datum stvaranja")]
        public DateTime DatumStvaranja { get; set; }
        [DisplayName("Datum dospijeca")]
        public DateTime DatumDospijeca { get; set; }
        [DisplayName("Cijena bez poreza")]
        public float UkupnaCijenaBezPoreza { get; set; }
        [DisplayName("Cijena sa porezom")]
        public float UkupnaCijenaSaPorezom { get; set; }
        public virtual string UserID { get; set; }
        [DisplayName("Primatelj racuna")]
        public string PrimateljRacuna { get; set; }
        public virtual ICollection<StavkeFakture> StavkeFakture { get; set; }
        public Faktura()
        {
            StavkeFakture = new HashSet<StavkeFakture>();
        }
        public enum PorezDrzave
        {
            Hrvatska,Bosna
        }
    }
}